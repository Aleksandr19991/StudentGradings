using Microsoft.AspNetCore.Authorization;
using StudentGradings.API.Models;
using StudentGradings.CORE;

namespace StudentGradings.API.Configuration;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    public CustomAuthorizeAttribute(UserRole[] roles)
    {
        Roles = string.Join(",", roles.Select(t => (int)t).ToArray());
    }
}
