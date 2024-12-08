namespace ConsoleApp1;

public class AnalyzedElement
{
    public string Name;
    public bool IsDiscovered;
    public List<HashSet<string>> Parents;
    public List<string> CreationWay;
    public int Difficulty;

    public override string ToString()
    {
        return $"{Name}: " +
            ( IsDiscovered ? "Starter " : "" ) +
            $"\n\tParents:\t[{string.Join( ", ", Parents.Select( y => string.Join( ", ", y ) ) )}], " +
            $"\n\tCreationWay:\t[\n\t\t\t\t{string.Join( ",\n\t\t\t\t", CreationWay )}\n\t\t\t], " +
            $"\n\tDifficulty:\t{Difficulty}";
    }
}