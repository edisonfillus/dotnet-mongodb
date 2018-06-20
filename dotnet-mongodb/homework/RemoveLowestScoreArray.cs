using model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace homework
{
    class RemoveLowestScoreArray
    {
       
        public static async Task DoRemove()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("school");
            var col = db.GetCollection<BsonDocument>("students");
            
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.And(builder.Lte("Age", 30), builder.Eq("Name", "Smith"));


            using (var cursor = await col.Find(new BsonDocument()).ToCursorAsync())
            {
                while (await cursor.MoveNextAsync())
                {
                    foreach (var student in cursor.Current)
                    {
                        BsonDocument lowestscore = null;
                        foreach (var score in student["scores"].AsBsonArray)
                        {
                            if (score["type"].AsString == "homework")
                            {
                                if (lowestscore == null || score["score"].AsDouble < lowestscore["score"].AsDouble)
                                {
                                    lowestscore = score.ToBsonDocument();
                                }
                            }
                        }
                        var result = await col.UpdateOneAsync(student, Builders<BsonDocument>.Update.Pull("scores", lowestscore));
                        Console.WriteLine(result.ToJson());
                    }
                }
            }

        }

        
    }
}
