using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace crud
{
    class Insert
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

        public static async Task InsertManyExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("people");

            var docs = Enumerable.Range(0, 10).Select(i => new BsonDocument("_id", i).Add("x", i));

            await col.InsertManyAsync(docs);

        }
    }
}