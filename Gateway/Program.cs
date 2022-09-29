using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", x =>
{
    x.RequireHttpsMetadata = false;
    x.Audience = builder.Configuration.GetSection("IdentityServerSettings:Audience").Value;
    x.Authority = builder.Configuration.GetSection("IdentityServerSettings:IdentityServerUrl").Value;
});


builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
});


builder.Services.AddOcelot();


var app = builder.Build();



app.UseOcelot();

app.Run();
