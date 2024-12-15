using Microsoft.AspNetCore.Authorization;
using StudentGradings.API.Models;

namespace StudentGradings.API.Configuration;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(UserRole[] roles)
    {
        Roles = string.Join(",", roles.Select(t => (int)t).ToArray());
    }
}
