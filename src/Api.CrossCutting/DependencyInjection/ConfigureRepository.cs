using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection servicecollection)
        {
            servicecollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            servicecollection.AddScoped<IUserRepository, UserImplementation>();

            servicecollection.AddDbContext<MyContext>(
                options => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbApiDotnetDDD;Trusted_Connection=True;MultipleActiveResultSets=true")
            );
        }
    }
}