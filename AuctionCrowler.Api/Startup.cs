using Tibia.Infrastructure.Repository.MongoDB;
using Tibia.Infrastructure.Adapters.Worlds;
using Tibia.Infrastructure.Adapters.CharAuctions;

namespace AuctionCrowler.Api
{
    public class Startup {
        public IConfiguration Configuration { get; set; }
        
        public Startup(IConfiguration configuration) {
            Configuration = configuration;            
        }

        public virtual void ConfigureServices(IServiceCollection services) {
            services.AddControllers();            
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            // Dependency injection
            services.AddSingleton<ICharAuctionSearchPageAdapter, CharAuctionSearchPageAdapter>();            
            services.AddSingleton<IWorldAdapter, WorldAdapter>();            

            // Mongo DB
            services.ConfigureMongoDB(Configuration);
        }

        public virtual void Configure(WebApplication app, IWebHostEnvironment env) {
            
            if (env.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

        }
    }
}
