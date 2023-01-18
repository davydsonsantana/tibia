using AuctionCrowler.Worker.Job.Interface;
using AuctionCrowler.Worker.Job;
using Hangfire;
using OpenQA.Selenium.Chrome;

namespace AuctionCrowler.Worker {
    public class Startup : AuctionCrowler.Api.Startup {
        public Startup(IConfiguration configuration) : base(configuration) { }

        public override void Configure(WebApplication app, IWebHostEnvironment env) {
            base.Configure(app, env);
            app.UseHangfireDashboard();
        }

        public override void ConfigureServices(IServiceCollection services) {
            var connectionString = Configuration["SqlServerHangfire:ConnectionString"];
            base.ConfigureServices(services);
            services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            services.AddHangfireServer();

            //Dependency Injection
            services.AddScoped<ICharAuctionCrowler, CharAuctionCrowler>();
            services.AddSingleton<IWorldCrowler, WorldCrowler>();
            services.AddTransient<ChromeDriver, ChromeDriver>();            
        }
    }
}
