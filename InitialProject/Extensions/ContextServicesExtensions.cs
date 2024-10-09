using System.Text.Json.Serialization;
using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.BusinessLayer.Services;
using Kawkaba.Core;
using Kawkaba.RepositoryLayer.Interfaces;
using Kawkaba.RepositoryLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kawkaba.Extensions;

public static class ContextServicesExtensions
{
    public static IServiceCollection AddContextServices(this IServiceCollection services, IConfiguration config)
    {

        //- context && json services
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));//,b => b.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)).UseLazyLoadingProxies());
        services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddControllersWithViews();
        services.AddSingleton<IRequestResponseService, RequestResponseService>();
        // IBaseRepository && IUnitOfWork Service
        //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>)); // only Repository
        services.AddTransient<IUnitOfWork, UnitOfWork>(); // Repository and UnitOfWork

        return services;
    }

}
