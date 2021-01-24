using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp1.Data;

namespace WebApp1.Configurations
{
    public static partial class ServicesConfiguration
    {
        public static void ConfigDB(
            this IServiceCollection services, 
            IConfiguration configuration,
            IWebHostEnvironment enviroment)
        {
            if (enviroment.EnvironmentName == "Development") {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("SqlServerDev")));
            }
            else {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("SqlServerProd")));
            }
            
          
        }  
        
    }
}