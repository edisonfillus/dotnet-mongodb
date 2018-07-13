using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace crud
{
    class FindOneAndUpdateReplaceDelete
    {


        public static async Task FindOneAndUpdateReplaceDeleteExample()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var db = client.GetDatabase("test");
            db.DropCollection("widgets");
            var col = db.GetCollection<Widget>("widgets");
            

            var docs = Enumerable.Range(0, 10).Select(i => new Widget { Id = i, X = i } );

            await col.InsertManyAsync(docs);

            // Find, Update and return the result before the update
            var result = await col.FindOneAndUpdateAsync(
                x => x.Id > 5,
                Builders<Widget>.Update.Inc(x => x.X, 1));
            Console.WriteLine(result.ToString());

            // Find, Update and return the result after the update
            var result2 = await col.FindOneAndUpdateAsync<Widget>(
                x => x.Id > 5,
                Builders<Widget>.Update.Inc(x => x.X, 1),
                new FindOneAndUpdateOptions<Widget, Widget>
                {
                    ReturnDocument = ReturnDocument.After
                });
            Console.WriteLine(result2.ToString());


            // Find, Update and return the result after the update, using a sorting by X (will get the 9)
            var result3 = await col.FindOneAndUpdateAsync<Widget>(
                x => x.Id > 5,
                Builders<Widget>.Update.Inc(x => x.X, 1),
                new FindOneAndUpdateOptions<Widget, Widget>
                {
                    ReturnDocument = ReturnDocument.After,
                    Sort = Builders<Widget>.Sort.Descending(x=>x.X)
                });
            Console.WriteLine(result3.ToString());

            // Find and replace
            var result4 = await col.FindOneAndReplaceAsync<Widget>(
                x => x.Id == 5,
                new Widget
                {
                     Id = 5,
                     X = 12
                },
                new FindOneAndReplaceOptions<Widget, Widget>
                {
                    ReturnDocument = ReturnDocument.Before,
                    Sort = Builders<Widget>.Sort.Descending(x => x.X)
                });
            Console.WriteLine(result4.ToString());

            // Find and delete
            var result5 = await col.FindOneAndDeleteAsync<Widget>(
                x => x.Id > 5,
                new FindOneAndDeleteOptions<Widget, Widget>
                {
                    Sort = Builders<Widget>.Sort.Descending(x => x.X)
                });
            Console.WriteLine(result5.ToString());


        }

        [BsonIgnoreExtraElements] // If you want to discard new elements in case of compatibilities
        private class Widget
        {
            public int Id { get; set; }

            [BsonElement("x")]
            public int X { get; set; }

            public override string ToString()
            {
                return "ID="+Id+",X="+X;
            }
        }


    }
}