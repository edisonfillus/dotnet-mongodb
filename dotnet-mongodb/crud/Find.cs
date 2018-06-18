using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace crud
{
    class Find
    {
       
        public static async Task FindExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            var col = db.GetCollection<BsonDocument>("people");

            // Using ForEachAsync, totaly async
            await col.Find(new BsonDocument()).ForEachAsync(doc => Console.WriteLine(doc));

            // Using ToListAsync, will put all in memory
            /*
            var list = await col.Find(new BsonDocument()).ToListAsync();
            foreach(var doc in list)
            {
                Console.WriteLine(doc);
            }
            */

            // Using cursor, no memory storage
            /*
            using (var cursor = await col.Find(new BsonDocument()).ToCursorAsync())
            {
                while(await cursor.MoveNextAsync())
                {
                    foreach(var doc in cursor.Current)
                    {
                        Console.WriteLine(doc);
                    }
                }
            }
            */

        }

        
    }
}
