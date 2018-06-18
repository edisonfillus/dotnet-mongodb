using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace crud
{
    class InsertOne
    {
        public static async Task InsertOneExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("people");
            var doc = new BsonDocument
            {
                { "Name", "Smith"},
                { "Age", 30 },
                { "Profession", "hacker" }
            };
            await col.InsertOneAsync(doc);
        }
    }
}