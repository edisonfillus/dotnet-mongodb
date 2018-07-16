using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;

namespace crud
{
    class BulkOperations
    {
        public static async Task BulkOperationsExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            db.DropCollection("widgets");
            var col = db.GetCollection<BsonDocument>("widgets");
            var docs = Enumerable.Range(0, 10).Select(i => new BsonDocument("_id",i).Add("x",i));
            await col.InsertManyAsync(docs);

            var result = col.BulkWriteAsync(new WriteModel<BsonDocument>[]
            {
                new DeleteOneModel<BsonDocument>("{x: 5}"),
                new DeleteOneModel<BsonDocument>("{x: 7}"),
                new UpdateOneModel<BsonDocument>("{x: {$lt: 7}}","{$inc: {x: 1}}")
            });

        }

    }

}