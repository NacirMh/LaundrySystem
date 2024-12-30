using LaundrySystem.WebApi.Business.Domain.Interfaces;
using LaundrySystem.WebApi.Business.Services;
using LaundrySystem.WebApi.Infrastructure.Daos;
using LaundrySystem.WebApi.Infrastructure.Data;
using LaundrySystem.WebApi.WebSocketLib;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ConDb")));
builder.Services.AddScoped<IConfigurationService, ConfigurationBusiness>();
builder.Services.AddScoped<IConfigurationDAO, ConfigurationDao>();
builder.Services.AddScoped<IMachineService, MachineManagement>();
builder.Services.AddScoped<IMachineDAO, MachineDao>();
builder.Services.AddScoped<IActionDAO, ActionDao>();
builder.Services.AddScoped<ICycleDAO, CycleDao>();
builder.Services.AddScoped<IOwnerDao, OwnerDao>();
builder.Services.AddScoped<ILaundryDao, LaundryDao>();
builder.Services.AddScoped<ILaundryService, LaundryManagement>();
builder.Services.AddScoped<IJWTTokenManager, JWTTokenManager>();
builder.Services.AddScoped<WebSocketHandler>();
builder.Services.AddSingleton<WebSocketConnectionManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors((configPolicy) =>
{
    configPolicy.AllowAnyMethod();
    configPolicy.AllowAnyOrigin();
    configPolicy.AllowAnyHeader();

});

app.UseWebSockets();

app.UseWebSocketMiddleWare();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
