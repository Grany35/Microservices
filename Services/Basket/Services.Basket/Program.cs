using Microservice.Shared.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Services.Basket.Services;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var requireAuthorizePolicy=new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
{
    x.Authority = builder.Configuration.GetSection("IdentityServerSettings:IdentityServerUrl").Value;
    x.Audience = builder.Configuration.GetSection("IdentityServerSettings:Audience").Value;
    x.RequireHttpsMetadata = false;
});


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityManager>();
builder.Services.AddScoped<IBasketService, BasketManager>();

builder.Services.AddSingleton<RedisManager>(x =>
{
    var redisHost = builder.Configuration.GetSection("RedisSettings:Host").Value;
    var redisPort = builder.Configuration.GetSection("RedisSettings:Port").Value;

    var redis = new RedisManager(redisHost, Convert.ToInt32(redisPort));

    redis.Connect();

    return redis;
});

builder.Services.AddControllers(X =>
{
    X.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
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
