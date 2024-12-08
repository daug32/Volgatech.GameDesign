using System.Text.Json;

namespace ConsoleApp1;

public static class Program
{
    private static readonly string _elementsJson =
        "D:\\Development\\Projects\\Volgatech.GameDesign\\game\\Assets\\Resources\\Database\\elements\\default.json";

    public static void Main()
    {
        var jsonData = File.ReadAllText( _elementsJson );
        var elements = JsonSerializer.Deserialize<Dictionary<string, Element>>( jsonData ) ?? throw new Exception();

        FindWay( elements );
    }

    private static void FindWay( Dictionary<string, Element> elements )
    {
        var starterElements = elements
           .Where( x => x.Value.isDiscovered )
           .Select( x => x.Key )
           .ToHashSet();

        Dictionary<(string, string), string> reactionTable = new();
        foreach ( var element in elements )
        {
            var elementName = element.Key;
            foreach ( var parents in element.Value.parents.Select( x => x.OrderBy( y => y ).ToList() ).ToList() )
            {
                reactionTable.Add( ( parents.First(), parents.Last() ), elementName );
            }
        }

        var possibleElements = LevelGenerator.FindAchievableElementsWithDifficulty( reactionTable, starterElements );
        var creationSteps = possibleElements.ToDictionary(
            x => x.Key,
            x => LevelGenerator.FindWay( starterElements, x.Key, reactionTable ) );

        var analyzedElements = elements.Select( pair =>
        {
            var (elementName, element) = pair;
            return new AnalyzedElement
            {
                Parents = element.parents,
                IsDiscovered = element.isDiscovered,
                Name = elementName,
                CreationWay = creationSteps.GetValueOrDefault( elementName, new List<string>() ),
                Difficulty = possibleElements.GetValueOrDefault( elementName, -1 ),
            };
        } ).ToList();

        var json = JsonSerializer.Serialize(
            BuildLevels( analyzedElements ),
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            } );

        Console.WriteLine( json );
    }

    private static Dictionary<string, Level> BuildLevels( IEnumerable<AnalyzedElement> elements )
    {
        var levels = new Dictionary<string, Level>();

        var levelNumber = 0;
        foreach ( var element in elements
                    .Where( x => x.Difficulty > 0 )
                    .OrderBy( x => x.Difficulty )
                    .ThenBy( x => x.Name ) )
        {
            var createByTime_2stars = Math.Max( element.CreationWay.Count * 3, 10 );
            var createByTime_3stars = Math.Max( element.CreationWay.Count * 6, 15 );
            
            levels.Add( $"level_{levelNumber++}", new Level
            {
                Targets = [ element.Name, ],
                Objectives =
                [
                    new Objective
                    {
                        Type = "FinishByReactions",
                        Data = element.CreationWay.Count.ToString(),
                    },
                    new Objective
                    {
                        Type = "FinishByTime",
                        Data = TimeSpan.FromSeconds( createByTime_2stars ).ToString(),
                    },
                    new Objective
                    {
                        Type = "FinishByTime",
                        Data = TimeSpan.FromSeconds( createByTime_3stars ).ToString(),
                    },
                ],
            } );
        }

        return levels;
    }
}