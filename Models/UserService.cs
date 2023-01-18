using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication2.Models
{
    public class UserService
    {
        private readonly IMongoCollection<BsonDocument> collection;
        public UserService(IOptions<UserDatabaseClass> option)
        {
            var dbclient = new MongoClient("mongodb://localhost:27017");
            var database = dbclient.GetDatabase("user");
            collection = database.GetCollection<BsonDocument>("user_detail");
        }

        public async Task Addocs(BsonDocument doc)
        {
            await collection.InsertOneAsync(doc);
        }
        public async Task<List<BsonDocument>> Get()
        {
            return await collection.Find(new BsonDocument()).Sort("{_id:1}").ToListAsync();
        }

        }
}
