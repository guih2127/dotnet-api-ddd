using System;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {
            
        }
    }

    public class DbTeste : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", String.Empty)}";
        public ServiceProvider serviceProvider {get; private set;}
        
        public DbTeste() 
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o => 
                o.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={dataBaseName};Trusted_Connection=True;MultipleActiveResultSets=true"),
                ServiceLifetime.Transient
            );

            serviceProvider  = serviceCollection.BuildServiceProvider();
            using (var context = serviceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }

        public void Dispose()
        {
            using (var context = serviceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    } 
}
