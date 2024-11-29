using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StudentGradings.CORE;
using System.Text;

namespace StudentGradings.API.Configuration;

internal static class AddAuthConfiguration
{
    internal static void AddAuth(this IServiceCollection services)
    {
           services
           .AddAuthentication(opt =>
           {
              opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
              opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
          .AddJwtBearer(options =>
          {
              options.TokenValidationParameters = new TokenValidationParameters
              {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = ConstantAuth.Issuer,
                 ValidAudience = ConstantAuth.Audience,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstantAuth.Key))
              };
          });
    }
}
