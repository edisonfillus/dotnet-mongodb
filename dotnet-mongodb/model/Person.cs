using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace model
{
    class Person
     {
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Profession { get; set; }

        public List<string> Colors { get; set; }

        public List<Pet> Pets { get; set; }
     }
}
