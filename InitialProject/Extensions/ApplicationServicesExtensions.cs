using Kawkaba.BusinessLayer.AutoMapper;
using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.BusinessLayer.Services;
using Kawkaba.Core.Entity.Posts;
using Kawkaba.Core.Entity.RequestEmployee;
using Kawkaba.Core.Helpers;
using System.Configuration;

namespace Kawkaba.Extensions;

public static class ApplicationServicesExtensions
{
    // interfaces sevices [IAccountService, IPhotoHandling  ]
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {

        services.AddDistributedMemoryCache(); // Add this line to configure the distributed cache

        // Session Service
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromHours(12);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
        services.AddTransient<IAccountService, AccountService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<ICompanyService, CompanyService>();
        services.AddTransient<IPostService, PostService>();
        services.AddTransient<IFileHandling, FileHandling>();
        services.AddTransient<IAgoraService, AgoraService>();
        services.AddTransient<IDashboardService, DashboardService>();
        services.Configure<AgoraSettings>(config.GetSection("AgoraSettings"));

        services.AddHttpClient();
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }

    public static IApplicationBuilder UseApplicationMiddleware(this IApplicationBuilder app)
    {
        app.UseSession();
        /*   app.UseHangfireDashboard("/Hangfire/Dashboard");

           app.UseWebSockets();*/

        return app;
    }
}
