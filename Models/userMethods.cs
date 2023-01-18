using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApplication2.Models
{
    public class userMethods
    {
        private readonly IMongoCollection<BsonDocument> collection;
        public userMethods(IOptions<userDatabase> option)
        {
            var mongoClient = new MongoClient(
                option.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                option.Value.DatabaseName);

            collection = mongoDatabase.GetCollection<BsonDocument>(
                option.Value.CollectionName);
        }

        public async Task Add(BsonDocument doc)
        {
            await collection.InsertOneAsync(doc);
        }

        public async Task<List<BsonDocument>> GetAsync()
        {
            return (List<BsonDocument>)collection.Find(new BsonDocument()).Sort("{Id:-1}").Limit(1);
        }
        
    }
}
