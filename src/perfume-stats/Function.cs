using Amazon.Lambda.Core;
using Perfume.Models;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace perfume_stats;

public class Function
{
    public string FunctionHandler(List<PerfumeDbItem> items, ILambdaContext context)
    {
        Console.WriteLine($"Found {items.Count} perfumes.");
        var perfumes = items.Select(p => new Perfume.Models.Perfume(p));
        perfumes = perfumes.OrderByDescending(p => p.Wearings?.Count ?? 0).ThenBy(p => p.House).ThenBy(p => p.Name);
        Console.WriteLine($"Mapped {perfumes.Count()} perfumes.");
        var stats = perfumes.Select(p => BuildPerfumeEntry(p));
        return string.Join('\n', stats);
    }

    private string BuildPerfumeEntry(Perfume.Models.Perfume perfume)
    {
        var writer = new StringWriter();
        var wearings = perfume.Wearings != null ? perfume.Wearings.Count.ToString() : "Not worn";
        var lastWorn = perfume.Wearings?.Last().ToString("dd/MM/yyyy") ?? "Never";
        writer.WriteLine(WriteEntry("House", perfume.House));
        writer.WriteLine(WriteEntry("Name:", perfume.Name));
        writer.WriteLine(WriteEntry("Wearings:", wearings));
        writer.WriteLine(WriteEntry("Last worn:", lastWorn));
        writer.Write(new string('-', 32));
        return writer.ToString();
    }

    private string WriteEntry(string key, string value)
    {
        return $"{key}{value.PadLeft(32-key.Length)}";
    }
}
