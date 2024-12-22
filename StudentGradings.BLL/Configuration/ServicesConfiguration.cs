using Microsoft.Extensions.DependencyInjection;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Mappings;

namespace StudentGradings.BLL.Configuration;

public static class ServicesConfiguration
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<ICoursesService, CoursesService>();
        services.AddScoped<IGradeBooksService, GradeBooksService>();
        services.AddScoped<IGraduatesService, GraduatesService>();
        services.AddAutoMapper(typeof(UserMapperProfile).Assembly);
    }
}
