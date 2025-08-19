using AutoMapper;
using WebApplication1.Data.Entities;
using WebApplication1.Dto;

namespace WebApplication1.Utils;

public static class UserUtils
{
    public static User MapUserForRegistrationIntoUser(UserForRegistration userForRegistration)
    {
        var config = new MapperConfiguration(config => { config.CreateMap<UserForRegistration, User>(); });

        var mapper = config.CreateMapper();
        var user = mapper.Map<UserForRegistration, User>(userForRegistration);
        return user;
    }
}
