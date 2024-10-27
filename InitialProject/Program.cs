using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kawkaba.Core;
using Kawkaba.Middleware;
using Kawkaba.Extensions;
using Kawkaba.BusinessLayer.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Optimize DB Context with connection resiliency
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

// Configure API behavior and services
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true); // Validation Error API
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add SignalR and other services
builder.Services.AddSignalR();
builder.Services.AddMemoryCache();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Register the services [IAccountService, IPhotoHandling, AddAutoMapper, Hangfire, etc.]
// Register Application and Identity services only once
builder.Services.AddContextServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Configure cookies
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(1); // Set cookie timeout to 1 hour
    options.SlidingExpiration = true;              // Reset cookie lifetime with each request
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
});

// Configure CORS
builder.Services.AddCors(options => {
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});

// Swagger Service
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ExceptionMiddleware>();
}

app.UseSwaggerDocumentation();
app.UseCors("CORSPolicy");
app.UseRouting();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseApplicationMiddleware();
app.UseAuthorization();
app.UseMiddleware<RequestResponseLoggingMiddleware>();

// Define endpoints
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Auth}/{action=Login}/{id?}");
});

app.Run();
