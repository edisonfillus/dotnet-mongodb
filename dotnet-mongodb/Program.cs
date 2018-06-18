using crud;
using System;

class Program
{

    static void Main(string[] args)
    {
        //CreatingDocuments.Documents().GetAwaiter().GetResult();
        //CreatingDocuments.ObjectMappings().GetAwaiter().GetResult();
        //InsertOne.InsertOneExample().GetAwaiter().GetResult();
        Find.FindExample().GetAwaiter().GetResult();

        Console.WriteLine("Press Enter");
        Console.ReadLine();
    }
}
