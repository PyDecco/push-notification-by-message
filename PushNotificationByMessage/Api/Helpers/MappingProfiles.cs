using AutoMapper;
using PushNotificationByMessage.Api.Dtos;
using PushNotificationByMessage.Core.Entities;

namespace PushNotificationByMessage.Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserToReturnDto>();
        }
    }
}
