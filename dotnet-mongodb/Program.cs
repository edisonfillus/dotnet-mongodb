using crud;
using homework;
using System;

class Program
{

    static void Main(string[] args)
    {
        //CreatingDocuments.Documents().GetAwaiter().GetResult();
        //CreatingDocuments.ObjectMappings().GetAwaiter().GetResult();
        //InsertOne.InsertOneExample().GetAwaiter().GetResult();
        //Find.FindExample().GetAwaiter().GetResult();
        //FindFilter.FindFilterObjectMapExample().GetAwaiter().GetResult();
        //FindSkipLimitSort.FindSkipLimitSortBsonExample().GetAwaiter().GetResult();
        //FindProjection.FindProjectionBsonExample().GetAwaiter().GetResult();
        //RemoveLowestScoreArray.DoRemove().GetAwaiter().GetResult();
        FindOneAndUpdateReplaceDelete.FindOneAndUpdateReplaceDeleteExample().GetAwaiter().GetResult();


        Console.WriteLine("Press Enter");
        Console.ReadLine();
    }
}
