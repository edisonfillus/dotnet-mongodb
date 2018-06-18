using model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace crud
{
    class FindFilter
    {
       
        public static async Task FindFilterExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            //var col = db.GetCollection<BsonDocument>("people"); When using BsonDocument
            var col = db.GetCollection<Person>("people");


            // Filters can be set as Json strings 
            //var filter = "{Name: 'Smith}";

            // Filters can be set as BsonDocument
            /*
             * var filter = new BsonDocument("Name", "Smith");
             * 
             * var filter = new BsonDocument("Name", "Smith").Add("Age",new BsonDocument("$lte", 30));
             * 
             * var filter = new BsonDocument("$or", new BsonArray
             * {
             *    new BsonDocument("Name", "Smith"),
             *    new BsonDocument("Age", new BsonDocument("$lte", 30))
             * });
             */

            // Filters can be set as Builders BsonDocument
            //var builder = Builders<BsonDocument>.Filter;
            //var filter = builder.Lte("Age", 30);
            //var filter = builder.And(builder.Lte("Age", 30), builder.Eq("Name", "Smith"));
            //var filter = builder.Lte("Age", 30) & !builder.Eq("Name", "Jones");

            var builder = Builders<Person>.Filter;
            var filter = builder.Lte(x => x.Age, 30) & !builder.Eq(x => x.Name, "Jones");

            var list = await col.Find(filter).ToListAsync();
            foreach(var doc in list)
            {
                Console.WriteLine(doc);
            }

        }

        
    }
}
