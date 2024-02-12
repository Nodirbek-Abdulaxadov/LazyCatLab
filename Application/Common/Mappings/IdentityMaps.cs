using Application.DTOs.ApplicationUserDtos;
using Domain.Entities.Identity;

namespace Application.Common.Mappings;

public static class IdentityMaps
{
    public static ApplicationUser ToApplicationUser(this RegisterUser registerUser)
    {
        return new ApplicationUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            FirstName = registerUser.FirstName,
            LastName = registerUser.LastName,
            PhoneNumber = registerUser.PhoneNumber,
            EmailConfirmed = false,
        };
    }
}