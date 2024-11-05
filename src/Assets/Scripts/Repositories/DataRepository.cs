using System;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Repositories.Dtos;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Repositories
{
    internal static class DataRepository
    {
        private static DataDto _data;
        
        public static void LoadForLevel( LevelType levelType )
        {
            string jsonData = JsonHelper.MergeJsons(
                JsonHelper.LoadJson( $"{Config.ElementsDataDatabase}/default" ),
                JsonHelper.LoadJson( $"{Config.ElementsDataDatabase}/{levelType.ToDatabaseFilename()}" ) );

            _data = LoadData( jsonData );
        }

        public static DataDto Get() => _data ?? throw new InvalidOperationException( "Data was not loaded yet" );

        private static DataDto LoadData( string json ) =>
            JsonHelper.Deserialize<DataDto>( json ) ??
            throw new ArgumentException( $"Failed to load data. Asset: {json}" );
    }
}