using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Tibia.Domain.Adapters;
using Tibia.Infrastructure.Adapters;
using Tibia.Infrastructure.Repository.MongoDB;
using Tibia.Infrastructure.Worker;
using Tibia.Infrastructure.Worker.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Dependency injection
builder.Services.AddSingleton<IWorldAdapter, WorldAdapter>();
builder.Services.AddSingleton<IWorldCrowler, WorldCrowler>();

// Mongo DB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();