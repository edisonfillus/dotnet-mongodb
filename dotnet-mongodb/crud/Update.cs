using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace crud
{
    class Update
    {

        public static async Task ReplaceOneExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("people");

            var docs = Enumerable.Range(0, 10).Select(i => new BsonDocument("_id", i).Add("x", i));

            await col.InsertManyAsync(docs);

            // Replace object with id
            var result = await col.ReplaceOneAsync(
                new BsonDocument("_id", 5),
                new BsonDocument("_id", 5).Add("x", 30)
            );

            // Replace object if exists, or else, insert
            var result2 = await col.ReplaceOneAsync(
                new BsonDocument("x", 10),
                new BsonDocument("x", 30),
                new UpdateOptions { IsUpsert = true }
            );


            // Builder also can be used
            var result3 = await col.ReplaceOneAsync(
                Builders<BsonDocument>.Filter.Eq("x",5),
                new BsonDocument("x", 30),
                new UpdateOptions { IsUpsert = true }
            );

            await col.Find(new BsonDocument()).ForEachAsync(x => Console.WriteLine(x));
        }


        public static async Task UpdateOneExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("people");

            var docs = Enumerable.Range(0, 10).Select(i => new BsonDocument("_id", i).Add("x", i));

            await col.InsertManyAsync(docs);

            var result = await col.UpdateOneAsync(
                Builders<BsonDocument>.Filter.Eq("x", 5),
                new BsonDocument("$inc", new BsonDocument("x",10))
            );

            // Builder can alse be used
            var result2 = await col.UpdateOneAsync(
             Builders<BsonDocument>.Filter.Eq("x", 5),
             Builders<BsonDocument>.Update.Inc("x",10)
            );

            await col.Find(new BsonDocument()).ForEachAsync(x => Console.WriteLine(x));
        }

        public static async Task UpdateManyExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("people");

            var docs = Enumerable.Range(0, 10).Select(i => new BsonDocument("_id", i).Add("x", i));

            await col.InsertManyAsync(docs);

            var result = await col.UpdateManyAsync(
                Builders<BsonDocument>.Filter.Gte("x", 5),
                new BsonDocument("$inc", new BsonDocument("x", 10))
            );

            // Builder can alse be used
            var result2 = await col.UpdateManyAsync(
             Builders<BsonDocument>.Filter.Gte("x", 5),
             Builders<BsonDocument>.Update.Inc("x", 10)
            );

            await col.Find(new BsonDocument()).ForEachAsync(x => Console.WriteLine(x));
        }

        public static async Task UpdateMappedObjectsExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<Widget>("widgets");

            var docs = Enumerable.Range(0, 10).Select(i => new Widget { Id = i, X = i } );

            await col.InsertManyAsync(docs);

            var result = await col.UpdateManyAsync(
             Builders<Widget>.Filter.Gte("x", 5),
             Builders<Widget>.Update.Inc("x", 10)
            );

            // Expressions can also be used
            var result2 = await col.UpdateManyAsync(
             Builders<Widget>.Filter.Gte(x => x.X, 5),
             Builders<Widget>.Update.Inc(x => x.X, 10)
            );

            // Expressions can also be used
            var result3 = await col.UpdateManyAsync(
             x=> x.X > 5,
             Builders<Widget>.Update.Inc(x => x.X, 10)
            );

            await col.Find(new BsonDocument()).ForEachAsync(x => Console.WriteLine(x.ToJson()));
        }

        [BsonIgnoreExtraElements] // If you want to discard new elements in case of compatibilities
        private class Widget
        {
            public int Id { get; set; }

            [BsonElement("x")]
            public int X { get; set; }
        }


    }
}