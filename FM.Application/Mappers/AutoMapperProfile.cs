using AutoMapper;
using FM.Application.DTOs.Responses.Accounts;
using FM.Application.DTOs.Responses.Facebook.Groups;
using FM.Domain.Entities.Facebook;
using FM.Domain.Entities.Identity;

namespace FM.Application.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, AccountResponse>();

            CreateMap<ApplicationUser, AuthenticationResponse>();

            #region Facebook
            CreateMap<PostGroupActionDto, FbPostGroupAction>();

            CreateMap<PostGroupCommentDto, FbPostGroupComment>()
                .ForMember(dest => dest.FbId, act => act.MapFrom(src => src.Id));

            CreateMap<PostGroupPrivacyDto, FbPostGroupPrivacy>();

            CreateMap<PostGroupDto, FbPostGroup>()
                .ForMember(dest => dest.PostGroupActions,
                    act => act.MapFrom(src => src.Actions))

                .ForMember(dest => dest.PostGroupComments, act => act.MapFrom(src => src.PostGroupCommentsDto.Data))

                .ForMember(dest => dest.PostGroupPrivacy, act => act.MapFrom(src => src.PostGroupPrivacyDto))

                .ForMember(dest => dest.ShareCount, act => act.MapFrom(src => src.PostGroupSharesDto.Count));
            #endregion
        }
    }
}