using System;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Extensions;
using Assets.Scripts.Repositories.Dtos;
using Assets.Scripts.Repositories.Dtos.Events;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Repositories
{
    internal static class DataRepository
    {
        private static DataDto _data;
        public static bool IsInitialized;
        
        public static void LoadForLevel( LevelType levelType )
        {
            string jsonData = JsonHelper.MergeJsons(
                JsonHelper.LoadJson( $"{Config.ElementsDataDatabase}/default" ),
                JsonHelper.LoadJson( $"{Config.ElementsDataDatabase}/{levelType.ToDatabaseFilename()}" ) );
            _data = LoadData( jsonData );
            IsInitialized = true;
            DataLoadedEventManager.Trigger();
        }

        public static DataDto Get() => _data ?? throw new InvalidOperationException( "Data was not loaded yet" );

        private static DataDto LoadData( string json ) =>
            JsonHelper.Deserialize<DataDto>( json ) ??
            throw new ArgumentException( $"Failed to load data. Asset: {json}" );
    }
}