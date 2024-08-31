using CategoryService.AsyncDataServices;
using CategoryService.Data;
using CategoryService.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();
builder.Services.AddGrpc();

builder.Services.AddDbContext<CategoryContext>(o => o.UseInMemoryDatabase("CategoryService"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcCategoryService>();

app.MapGet("/protos/categories.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Protos/categories.proto"));
                });

// default route
app.MapGet("/", () => "ControllerServices is working");

// seeding data
await DbSeeder.SeedData(app);

app.Run();
