using model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace crud
{
    class FindProjection
    {
       
        public static async Task FindProjectionBsonExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("people");

            // Project can be set as JSon
            //var project = "{Name: 1, _id: 0}";

            // Project can be set as BSonDocument
            //var project = new BsonDocument("Name", 1).Add("_id", 0);

            // Project can be set with Buiders
            var project = Builders<BsonDocument>.Projection.Include("Name").Exclude("_id");
            
            var list = await col.Find(new BsonDocument())
                .Project(project)
                .ToListAsync();
                                    
            foreach (var doc in list)
            {
                Console.WriteLine(doc.ToJson());
            }

        }

        public static async Task FindProjectObjectMapExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<Person>("people");

            // Will return a Bson
            var list = await col.Find(x => true)
                .Project(Builders<Person>.Projection.Include("Name").Exclude("_id"))
                .ToListAsync();

            // Will return a Person
            var list2 = await col.Find(x => true)
                .Project<Person>(Builders<Person>.Projection.Include("Name").Exclude("_id"))
                .ToListAsync();

            // Using expression trees, return a Person
            var list3 = await col.Find(x => true)
                .Project<Person>(Builders<Person>.Projection.Include(x => x.Name).Exclude(x => x.Id))
                .ToListAsync();

            // Return a List<string>
            var list4 = await col.Find(x => true)
                .Project(x => x.Name)
                .ToListAsync();

            // Return a Calculated Result (client side) using anonimous class
            var list5 = await col.Find(x => true)
                .Project(x => new { x.Name, CalcAge = x.Age + 20 })
                .ToListAsync();


            foreach (var doc in list)
            {
                Console.WriteLine(doc.ToJson());
            }

        }



    }
}
