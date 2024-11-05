namespace Assets.Scripts.Application.Levels
{
    internal static class LevelTypeExtensions 
    {
        public static string ToDatabaseFilename( this LevelType levelType ) => levelType.ToString().ToLower();
    }
}
