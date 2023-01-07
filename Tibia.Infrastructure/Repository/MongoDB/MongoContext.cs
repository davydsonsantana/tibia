using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibia.MongoDB;

namespace Tibia.Infrastructure.Repository.MongoDB {
    public class MongoContext : IMongoContext {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public IMongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        private readonly IConfiguration _configuration;

        public MongoContext(IMongoClient mongoCliente, IConfiguration configuration) {
            _configuration = configuration;
            MongoClient = mongoCliente;
            Database = MongoClient.GetDatabase(_configuration["MongoDB:DatabaseName"]);

            // Every command will be stored and it'll be processed at SaveChanges
            _commands = new List<Func<Task>>();
        }
        public async Task<int> SaveChanges() {
            using (Session = await MongoClient.StartSessionAsync()) {
                Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

                await Session.CommitTransactionAsync();
            }

            return _commands.Count;
        }

        public IMongoCollection<T> GetCollection<T>(string name) {
            return Database.GetCollection<T>(name);
        }

        public void Dispose() {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public void AddCommand(Func<Task> func) {
            _commands.Add(func);
        }
    }
}
