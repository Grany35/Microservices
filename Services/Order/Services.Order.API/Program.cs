using MediatR;
using Microservice.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Services.Order.Infrasturcture;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.Authority = builder.Configuration.GetSection("IdentityServerSettings:IdentityServerUrl").Value;
    x.Audience = builder.Configuration.GetSection("IdentityServerSettings:Audience").Value;
    x.RequireHttpsMetadata = false;
});


builder.Services.AddDbContext<OrderDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), configure =>
    {
        configure.MigrationsAssembly("Services.Order.Infrasturcture");
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(typeof(Services.Order.Application.Handlers.CreateOrderCommandHandler).Assembly);

builder.Services.AddScoped<ISharedIdentityService, SharedIdentityManager>();




builder.Services.AddControllers(x =>
{
    x.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
