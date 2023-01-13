using DnsClient.Internal;
using Microsoft.Extensions.Logging;
using Tibia.Domain.Comunity;
using Tibia.Domain.Repository;
using Tibia.Infrastructure.Repository.MongoDB;
using Tibia.MongoDB;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole().AddDebug();

using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
    .AddConsole());

ILogger logger = loggerFactory.CreateLogger<Program>();
logger.LogInformation("Example log message");

// Mongo DB
builder.Services.ConfigureMongoDB(builder.Configuration, logger);

var app = builder.Build();

var context = app.Services.GetService<IMongoContext>();
var DbSet = context.GetCollection<World>(typeof(World).Name.ToLower());

var world = new World("Dibra", ELocation.SouthAmerica, EPvpType.OpenPVP, EBattleEye.Protected);

var worldRepository = app.Services.GetService<IWorldRepository>();
worldRepository.InsertOne(world);


context.Dispose();

app.Run();

