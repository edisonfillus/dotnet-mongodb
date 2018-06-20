using model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace crud
{
    class FindSkipLimitSort
    {
       
        public static async Task FindSkipLimitSortBsonExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("people");
                
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.And(builder.Lte("Age", 30), builder.Eq("Name", "Smith"));

            // Sort can be set using Json
            //var sort = "{Age: 1}";

            // Sort can be set using BsonDocument
            //var sort = new BsonDocument("Age", 1);

            // Sort can be set using Builders
            var sort = Builders<BsonDocument>.Sort.Ascending("Age").Descending("Name");

            var list = await col.Find(filter)
                .Sort(sort)
                .Limit(1)
                .Skip(0)
                .ToListAsync();
                                    
            foreach (var doc in list)
            {
                Console.WriteLine(doc.ToJson());
            }

        }

        public static async Task FindFilterObjectMapExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<Person>("people");
            
            var builder = Builders<Person>.Filter;
            var filter = builder.Lte(x => x.Age, 30) & !builder.Eq(x => x.Name, "Jones");

            // Can set fields as strings
            //var sort = Builders<Person>.Sort.Ascending("Age").Descending("Name");

            // Can set fields as lambdas
            var sort = Builders<Person>.Sort.Ascending(x => x.Age).Descending(x => x.Name);

            var list = await col.Find(filter)
                .Sort(sort)
                .Skip(0)
                .Limit(1)
                .ToListAsync();

            // Sort can be set as Expressions Trees
            var list2 = await col.Find(filter)
                .SortBy(x => x.Age).ThenByDescending(x => x.Name)
                .Skip(0)
                .Limit(1)
                .ToListAsync();
            

            foreach (var doc in list)
            {
                Console.WriteLine(doc.ToJson());
            }

        }



    }
}
