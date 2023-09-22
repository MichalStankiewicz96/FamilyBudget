
using FamilyBudget.Api.Authentication;
using FamilyBudget.Api.Extensions.Options;
using FamilyBudget.Api.Extensions.Swagger;
using FamilyBudget.Application.Extensions;
using FamilyBudget.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

Log.Information("Starting up");

try
{
    RunApplication();
}

catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}

void RunApplication()
{
    var builder = WebApplication.CreateBuilder(args);
    builder.WebHost.CaptureStartupErrors(true);
    // Add services to the container.
    builder.Services.AddApplicationOptions(builder.Configuration);
    builder.Services.AddApplication(builder.Configuration);
    builder.Services.AddAuthenticationExtension(builder.Configuration);

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwagger();

    var app = builder.Build();

    ApplyMigrations(app);

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsProduction())
    {
        app.UseSwaggerUi();
    }
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

void ApplyMigrations(WebApplication webApplication)
{
    using var scope = webApplication.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}