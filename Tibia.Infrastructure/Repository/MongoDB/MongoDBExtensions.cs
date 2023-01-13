using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;
using Tibia.Domain.Repository;
using Tibia.Infrastructure.Repository.MongoDB.Mapper;
using Tibia.MongoDB;

namespace Tibia.Infrastructure.Repository.MongoDB {
    public static class MongoDBExtensions {
        public static void ConfigureMongoDB(this IServiceCollection services, IConfiguration configuration, ILogger logger) {

            var connectionString = configuration["MongoDB:ConnectionString"];
            var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);
            mongoClientSettings.ClusterConfigurator = cb => {
                cb.Subscribe<CommandStartedEvent>(e => {
                    logger.LogTrace($"{e.CommandName} - {e.Command.ToJson()}");
                });
            };
            
            services.AddSingleton<IMongoClient, MongoClient>(sp => new MongoClient(mongoClientSettings));
                                    
            ConfigureDependencyInjection(services);
            ConfigureMappers();

            //BsonDefaults.GuidRepresentation = GuidRepresentation.CSharpLegacy;
            //BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.CSharpLegacy));

            // Conventions
            var pack = new ConventionPack
                {
                    new IgnoreExtraElementsConvention(true),
                    new IgnoreIfDefaultConvention(true)
                };
            ConventionRegistry.Register("My Solution Conventions", pack, t => true);
        }

        private static void ConfigureDependencyInjection(IServiceCollection services) {                        
            services.AddSingleton<IMongoContext, MongoContext>();
            
            // Repository
            services.AddSingleton<IWorldRepository, WorldRepository>();
        }

        private static void ConfigureMappers() {
            EntityMapper.Configure();
            WorldMapper.Configure();
        }

    }
}
