namespace ConsoleApp1;

public static class LevelGenerator
{
    public static List<string> FindWay(
        HashSet<string> starterElements,
        string targetElement,
        Dictionary<(string, string), string> reactionTable )
    {
        var queue = new Queue<(HashSet<string>, List<string>)>();
        var visited = new HashSet<HashSet<string>>( HashSet<string>.CreateSetComparer() );

        queue.Enqueue( ( [ ..starterElements, ], [ ] ) );
        visited.Add( [ ..starterElements, ] );

        while ( queue.Count > 0 )
        {
            var (currentElements, path) = queue.Dequeue();

            if ( currentElements.Contains( targetElement ) )
            {
                return path;
            }

            var elementsArray = new List<string>( currentElements );
            for ( var i = 0; i < elementsArray.Count; i++ )
            {
                for ( var j = i; j < elementsArray.Count; j++ )
                {
                    var element1 = elementsArray[ i ];
                    var element2 = elementsArray[ j ];

                    if ( !reactionTable.TryGetValue( ( element1, element2 ), out string newElement ) &&
                        !reactionTable.TryGetValue( ( element2, element1 ), out newElement ) )
                    {
                        continue;
                    }

                    var newElements = new HashSet<string>( currentElements ) { newElement, };
                    var newPath = new List<string>( path ) { $"{element1} + {element2} -> {newElement}", };

                    if ( visited.Contains( newElements ) )
                    {
                        continue;
                    }

                    queue.Enqueue( ( newElements, newPath ) );
                    visited.Add( newElements );
                }
            }
        }

        return new List<string>();
    }

    public static Dictionary<string, int> FindAchievableElementsWithDifficulty(
        Dictionary<(string, string), string> reactionTable,
        HashSet<string> starterElements )
    {
        var result = new Dictionary<string, int>();
        var visitedElements = new HashSet<string>();
        foreach ( var starterElement in starterElements )
        {
            visitedElements.Add( starterElement );
            result.Add( starterElement, 0 );
        }

        while ( true )
        {
            List<((string, string), string)> reactionsToProcess = reactionTable
               .Where( x => visitedElements.Contains( x.Key.Item1 ) && visitedElements.Contains( x.Key.Item2 ) )
               .Select( x => ( x.Key, x.Value ) )
               .ToList();
            if ( !reactionsToProcess.Any() )
            {
                break;
            }

            var hasChanges = false;
            foreach ( var reaction in reactionsToProcess )
            {
                var difficulty = Math.Max( result[ reaction.Item1.Item1 ], result[ reaction.Item1.Item2 ] ) + 1;
                visitedElements.Add( reaction.Item2 );

                if ( !result.ContainsKey( reaction.Item2 ) )
                {
                    result[ reaction.Item2 ] = difficulty;
                    hasChanges = true;
                    continue;
                }

                var oldDifficulty = result[ reaction.Item2 ];
                if ( oldDifficulty <= difficulty )
                {
                    continue;
                }

                result[ reaction.Item2 ] = difficulty;
                hasChanges = true;
            }

            if ( !hasChanges )
            {
                break;
            }
        }

        return result;
    }
}