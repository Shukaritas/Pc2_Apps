using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using pc2NRCuCodigo.API.Premium.Application.Internal.CommandServices;
using pc2NRCuCodigo.API.Premium.Application.Internal.QueryServices;
using pc2NRCuCodigo.API.Premium.Domain.Repositories;
using pc2NRCuCodigo.API.Premium.Domain.Services;
using pc2NRCuCodigo.API.Premium.Infrastructure.Persistence.EFC.Repositories;
using pc2NRCuCodigo.API.Shared.Domain.Repositories;
using pc2NRCuCodigo.API.Shared.Infrastructure.Interfaces.ASP.Configuration;
using pc2NRCuCodigo.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
using pc2NRCuCodigo.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using pc2NRCuCodigo.API.Shared.Infrastructure.Persistence.EFC.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers( options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod().AllowAnyHeader());
});

if (connectionString == null) throw new InvalidOperationException("Connection string not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Pc2NRCuCodigo API",
        Version = "v1",
        Description = "API for managing premium orders in Pc2NRCuCodigo platform.",
    });
    options.EnableAnnotations();
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPremiumOrderRepository, PremiumOrderRepository>();
builder.Services.AddScoped<IPremiumOrderCommandService, PremiumOrderCommandService>();
builder.Services.AddScoped<IPremiumOrderQueryService, PremiumOrderQueryService>();

builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));

builder.Services.AddCortexMediator(
    configuration: builder.Configuration,
    handlerAssemblyMarkerTypes: [typeof(Program)],
    configure: options =>
    {
        options.AddOpenCommandPipelineBehavior(typeof(LoggingCommandBehavior<>));
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
