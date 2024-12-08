using System;
using System.Linq;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Repositories.Levels;

namespace Assets.Scripts.Tests
{
    public static class LevelsValidator
    {
        public static void Validate()
        {
            LevelDataRepository.Load();

            foreach ( LevelType levelType in Enum.GetValues( typeof( LevelType ) ) )
            {
                ValidateLevelData( levelType );
            }
        }

        private static void ValidateLevelData( LevelType levelType )
        {
            var levelData = GetLevelData( levelType );

            if ( levelData.Objectives.Count != 3 )
            {
                throw new ArgumentException(
                    "Expected to be 3 objectives. " +
                    $"Objectives number: {levelData.Objectives.Count}, " +
                    $"Level: {levelType}" );
            }

            if ( !levelData.Targets.Any() )
            {
                throw new ArgumentException(
                    "Expected to be at least 1 level target. " +
                    $"Level: {levelType}" );
            }
        }

        private static LevelData GetLevelData( LevelType levelType )
        {
            LevelData levelData;
            try
            {
                levelData = LevelDataRepository.Get( levelType );
            }
            catch
            {
                throw new ArgumentException( $"No level data provided. Level: {levelType}" );
            }

            return levelData;
        }
    }
}