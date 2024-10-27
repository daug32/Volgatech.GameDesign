namespace Assets.Scripts.Models.Levels
{
    internal static class LevelTypeExtensions 
    {
        public static string ToDatabaseFilename( this LevelType levelType ) => $"{levelType.ToString().ToLower()}.json";
    }
}
