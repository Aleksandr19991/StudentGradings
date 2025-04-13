using FluentValidation;
using FluentValidation.AspNetCore;
using StudentGradings.API.Mappings;
using StudentGradings.API.Models.Requests.Validators;

namespace StudentGradings.API.Configuration
{
    public static class ServicesConfiguration
    {
        public static void AddApiServices(this IServiceCollection services)
        {
            services.AddAuth();
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddFluentValidationAutoValidation();
            //services.AddValidatorsFromAssemblyContaining<RegisterUserRequestValidator>();
            services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
        }
    }
}
