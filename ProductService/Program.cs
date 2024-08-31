using Microsoft.EntityFrameworkCore;
using ProductService.AsyncDataServices;
using ProductService.Data;
using ProductService.EventProcessing;
using ProductService.Middlewares;
using ProductService.SyncService.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductContext>(opt => opt.UseInMemoryDatabase("ProductList"));

builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddHostedService<MessageBusSubscriber>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddScoped<ICategoryDataClient, CategoryDataClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();

await DbSeeder.Seed(app);

app.Run();
