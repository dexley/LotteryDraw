using SIS.Lottery.Api.Infrastructure;
using SIS.Lottery.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SIS.Lottery.Api.Application;

namespace SIS.Lottery.Api.Infrastructure
{
    public class LotteryRepository : IRepository<LotteryDraw>
    {
        private readonly IMongoDatabase _database;

        public LotteryRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<IEnumerable<LotteryDraw>> GetAll()
        {
            var collection = await _database.GetCollection<LotteryDraw>("LotteryDraws").FindAsync<LotteryDraw>(new BsonDocument());
            return collection.ToEnumerable();
        }

        public async Task<LotteryDraw> Find(string name)
        {
            var collection = _database.GetCollection<LotteryDraw>("LotteryDraws");
            var filter = Builders<LotteryDraw>.Filter.Eq("Name", name);
            var result = await collection.FindAsync(filter).Result.ToListAsync();
            return result.FirstOrDefault();
        }

        public async Task Create(LotteryDraw t)
        {
            t._id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            var collection = _database.GetCollection<LotteryDraw>("LotteryDraws");
            await collection.InsertOneAsync(t);
        }

        public async Task Update(LotteryDraw t)
        {
            var collection = _database.GetCollection<LotteryDraw>("LotteryDraws");
            var filter = Builders<LotteryDraw>.Filter.Eq("Name", t.Name);
            var result = await collection.ReplaceOneAsync(filter, t);
        }
    }
}
