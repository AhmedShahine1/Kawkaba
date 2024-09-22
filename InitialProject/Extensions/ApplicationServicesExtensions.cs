using InitialProject.BusinessLayer.Interfaces;
using InitialProject.BusinessLayer.Services;

namespace InitialProject.Extensions;

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

        services.AddHttpClient();
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
