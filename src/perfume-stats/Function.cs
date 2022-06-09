using System.Collections.Immutable;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using Perfume.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace perfume_stats;

public class Function
{
    
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public string FunctionHandler(List<PerfumeDbItem> items, ILambdaContext context)
    {
        Console.WriteLine($"Found {items.Count} perfumes.");
        var perfumes = items.Select(p => new Perfume.Models.Perfume(p));
        perfumes = perfumes.OrderBy(p => p.House).ThenBy(p => p.Name);
        Console.WriteLine($"Mapped {perfumes.Count()} perfumes.");
        var stats = perfumes.Select(p => $"House: {p.House}\nName: {p.Name}\nWearings: {p.Wearings?.Count ?? 0}\nLast worn: {p.Wearings?.Last().ToString("dd MMMM yyyy")}").ToArray();
        return string.Join("\n\n", stats);
    }
}
