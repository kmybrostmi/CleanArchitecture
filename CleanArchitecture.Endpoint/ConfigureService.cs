using CleanArchitecture.Infrastructure.EfContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Endpoint;
public static class ConfigureService
{
    public static IServiceCollection AddWebConfigureService(this WebApplicationBuilder builder)
    {
        // Add services to the container.
        builder.Services.AddControllersWithViews();

        return builder.Services;
    }

    public async static Task<IApplicationBuilder> AddWebAppService(this WebApplication app)
    {
        //Create Scope
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;

        //Get Service
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var context = services.GetRequiredService<AppDbContext>();

        //Auto Migration And Generate SeedData
        try
        {
            await context.Database.MigrateAsync();
            //await GenerateFakeData.SeedDataAsync(context, loggerFactory);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex.Message);
        }

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();

        return app;
    }
}
