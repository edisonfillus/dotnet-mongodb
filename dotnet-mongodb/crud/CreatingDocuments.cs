using model;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crud
{
    class CreatingDocuments
    {

        public static async Task Documents()
        {
            // It's possible to use dictionaries
            var doc = new BsonDocument
            {
                {"name","Jones" }
            };

            // It's possible to use Add Method
            doc.Add("age", 30);

            // It's possible to use index form
            doc["profession"] = "hacker";

            var nestedArray = new BsonArray();
            nestedArray.Add(new BsonDocument("color", "red"));
            doc.Add("array", nestedArray);

            Console.WriteLine(doc["array"][0]["color"]);

            Console.WriteLine(doc);

        }

        public static async Task ObjectMappings()
        {
            var person = new Person
            {
                Name = "Jones",
                Age = 30,
                Colors = new List<string> { "red", "blue" },
                Pets = new List<Pet> { new Pet { Name = "Flufly", Type = "cat" } }
            };
            using (var writer = new JsonWriter(Console.Out))
            {
                BsonSerializer.Serialize(writer, person);
            }
        }

    }
}
