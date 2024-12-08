namespace Assets.Scripts.Application.Levels
{
    internal class LevelObjective
    {
        public readonly LevelObjectiveType Type;
        public string Data;

        public LevelObjective( LevelObjectiveType type, string data )
        {
            Type = type;
            Data = data;
        }
    }
}