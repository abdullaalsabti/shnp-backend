using System.Security.Claims;
using AutoMapper;
using WebApplication1.Data.Entities;
using WebApplication1.Dto;

namespace WebApplication1.Utils;

public static class UserUtils
{
    public static Restaurant MapUserForRegistrationIntoUser(UserForRegistrationDto userForRegistrationDto)
    {
        var config = new MapperConfiguration(config => { config.CreateMap<UserForRegistrationDto, Restaurant>(); });

        var mapper = config.CreateMapper();
        var user = mapper.Map<UserForRegistrationDto, Restaurant>(userForRegistrationDto);
        return user;
    }

    public static string? GetInfoFromClaim(ClaimsPrincipal user, string claimType)
    {
        return user.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;
    }
}
