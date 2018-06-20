using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace crud
{
    class Delete
    {


        public static async Task DeleteMappedObjectsExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<Widget>("widgets");

            var docs = Enumerable.Range(0, 10).Select(i => new Widget { Id = i, X = i } );

            await col.InsertManyAsync(docs);

            var result = await col.DeleteOneAsync("{ x: 5}");

            var result1 = await col.DeleteOneAsync(Builders<Widget>.Filter.Gte("x", 5));

            // Expressions can also be used
            var result2 = await col.DeleteOneAsync( Builders<Widget>.Filter.Gte(x => x.X, 5));

            // Expressions can also be used
            var result3 = await col.DeleteOneAsync( x => x.X > 5 );
            var result4 = await col.DeleteManyAsync(x => x.X > 5);

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