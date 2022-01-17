using FM.Application.Common.Interfaces;
using FM.Domain.Common;
using FM.Domain.Entities;
using FM.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ApplicationUser = FM.Domain.Entities.Identity.ApplicationUser;
using RefreshToken = FM.Domain.Entities.Identity.RefreshToken;

namespace FM.Infrastructure.Data.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<TodoItem> TodoItem { get; set; }

        public DbSet<TodoList> TodoLists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //SeedAccountDefault.SeedAccount(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entry in ChangeTracker.Entries<AudiTableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.Uid;
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService.Uid;
                        entry.Entity.UpdatedDate = DateTime.UtcNow;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}