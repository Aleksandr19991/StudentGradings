using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentGradings.DAL.Interfaces;

namespace StudentGradings.DAL.Configuration;

public static class ServicesConfiguration
{
    public static void AddDalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StudentGradingsContext>(options => options.UseNpgsql(configuration.GetConnectionString("StudentGradingsDbConnection")));
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ICoursesRepository, CoursesRepository>();
        services.AddScoped<IUserCoursesRepository, UserCoursesRepository>();
    }
}