namespace Assets.Scripts.Application.Menus.Arcades.Levels.Models
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