using ApiGateway.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddJwtAuthentication(builder.Configuration);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile(
        "ocelot.development.json",
        optional: false,
        reloadOnChange: true);
}
else
{
    builder.Configuration.AddJsonFile("ocelot.production.json", optional: false, reloadOnChange: true);
}

builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseOcelot().Wait();

app.MapControllers();

app.Run();
