using FM.Infrastructure.Data.Contexts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FM.Application.DTOs.Requests.Accounts;
using FM.Application.DTOs.Responses.Accounts;
using FM.Application.Enums;
using FM.Application.Exceptions;
using FM.Application.Wrappers;
using FM.Domain.Entities;
using FM.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FM.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTSettings _jwt;
        public AccountService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JWTSettings> jwt, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _jwt = jwt.Value;
        }

        public async Task<BaseResponse<AuthenticationResponse>> GetTokenAsync(TokenModelRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException($"No Accounts Registered with {request.Email}.");
            }
            //var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            var result = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!result)
            {
                throw new ApiException($"Invalid Credentials for '{request.Email}'.");
            }
            //if (!user.EmailConfirmed)
            //{
            //    throw new ApiException($"Account Not Confirmed for '{request.Email}'.");
            //}
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            AuthenticationResponse response = new AuthenticationResponse();

            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Email = user.Email;
            response.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();

            if (user.RefreshTokens.Any(a => a.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.FirstOrDefault(a => a.IsActive == true);
                response.RefreshToken = activeRefreshToken.Token;
                response.RefreshTokenExpiration = activeRefreshToken.Expires;
            } else
            {
                var refreshToken = CreateRefreshToken();
                response.RefreshToken = refreshToken.Token;
                response.RefreshTokenExpiration = refreshToken.Expires;
                user.RefreshTokens.Add(refreshToken);

                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            return new ResponseSuccess<AuthenticationResponse>((int)HttpStatusCode.OK, $"Authenticated {user.UserName}", response);
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();
            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public async Task<BaseResponse<string>> RegisterAsync(RegisterModelRequest request)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                //throw new ApiException($"Username '{request.UserName}' is already taken.");
                string msgError = $"Username '{request.UserName}' is already taken.";
                return new ResponseFail<string>((int)HttpStatusCode.BadRequest, msgError, new List<string> { msgError });
            }
            var user = new ApplicationUser
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };
            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                    return new ResponseSuccess<string>($"User Registered successfully with UserName = {request.UserName}, Email = {request.Email}");

                } else
                {
                    throw new ApiException($"{result.Errors}");
                }
            } else
            {
                string msgError = $"Email {request.Email } is already registered.";
                return new ResponseFail<string>((int)HttpStatusCode.BadRequest, msgError, new List<string> { msgError });
                //throw new ApiException($"Email {request.Email } is already registered.");
            }
        }

        public async Task<string> AddRoleAsync(AddRoleModelRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return $"No Accounts Registered with {model.Email}.";
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var roleExists = Enum.GetNames(typeof(Roles)).Any(x => x.ToLower() == model.Role.ToLower());
                if (roleExists)
                {
                    var validRole = Enum.GetValues(typeof(Roles)).Cast<Roles>().Where(x => x.ToString().ToLower() == model.Role.ToLower()).FirstOrDefault();
                    await _userManager.AddToRoleAsync(user, validRole.ToString());
                    return $"Added {model.Role} to user {model.Email}.";
                }
                return $"Role {model.Role} not found.";
            }
            return $"Incorrect Credentials for user {user.Email}.";
        }


        public async Task<AuthenticationResponse> RefreshTokenAsync(string token)
        {
            var authenticationModel = new AuthenticationResponse();
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Token did not match any users.";
                return authenticationModel;
            }
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"Token Not Active.";
                return authenticationModel;
            }
            //Revoke Current Refresh Token
            refreshToken.Revoked = DateTime.UtcNow;
            //Generate new Refresh Token and save to Database
            var newRefreshToken = CreateRefreshToken();
            user.RefreshTokens.Add(newRefreshToken);
            _context.Update(user);
            _context.SaveChanges();
            //Generates new jwt
            authenticationModel.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
            authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires;
            return authenticationModel;
        }
        public ApplicationUser GetById(string id)
        {
            return _context.Users.Find(id);
        }

        public async Task<bool> RevokeToken(string token)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
            // return false if no user found with token
            if (user == null) return false;
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);
            // return false if token is not active
            if (!refreshToken.IsActive) return false;
            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(10),
                    Created = DateTime.UtcNow
                };
            }
        }

        public async Task<string> UpdateRoleAsync(UpdateRoleModelRequest model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    return $"No Accounts Registered with {model.Email}.";
                }
                //Remove All Roles of user
                var allRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, allRoles);
                //Add new
                foreach (var role in model.Roles)
                {
                    var roleExists = Enum.GetNames(typeof(Roles)).Any(x => x.ToLower() == role.ToLower());
                    if (roleExists)
                    {
                        var validRole = Enum.GetValues(typeof(Roles)).Cast<Roles>().Where(x => x.ToString().ToLower() == role.ToLower()).FirstOrDefault();
                        await _userManager.AddToRoleAsync(user, validRole.ToString());
                    }
                }
                return $"Added all roles to user with email = {model.Email}";
            }
            catch (Exception ex)
            {
                return $"There is something went wrong. Message = {ex.Message}";
            }
        }
    }
}