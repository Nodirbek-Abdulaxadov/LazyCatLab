using Microsoft.AspNetCore.Identity;

namespace Application.Common.Mappings;
public static class ErrorMaps
{
    public static string ToErrorString(this IEnumerable<IdentityError> errors)
    {
        return string.Join('\n', errors.Select(e => e.Description));
    }
}