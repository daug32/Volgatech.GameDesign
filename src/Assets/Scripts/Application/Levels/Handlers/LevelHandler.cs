using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Application.Elements;
using Assets.Scripts.Application.Levels.Events;
using Assets.Scripts.Application.Ui;
using Assets.Scripts.Repositories;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
using Assets.Scripts.Repositories.Ui;
using Assets.Scripts.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Application.Levels.Handlers
{
    internal static class LevelHandler
    {
        public static void Initialize( LevelType levelType )
        {
            DataRepository.LoadForLevel( levelType );

            LevelData levelData = LevelDataRepository.Get();
            if ( !levelData.Targets.Any() )
            {
                return;
            }

            UserInterface userInterface = UiItemsRepository.GetUserInterface();
            foreach ( ElementId targetElement in levelData.Targets )
            {
                ElementsRepository
                   .Get( targetElement )
                   .CreateGameObject()
                   .WithParent( userInterface.TargetsContainer );
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public static IEnumerator CompleteLevelIfNeeded( LevelType levelType )
        {
            LevelData levelData = LevelDataRepository.Get();

            var areAllTargetsDiscovered = levelData.Targets.All( x => ElementsDataRepository.Get( x ).IsDiscovered );
            if ( !areAllTargetsDiscovered )
            {
                yield break;
            }

            var userInterface = UiItemsRepository.GetUserInterface();

            userInterface.SuccessText.SetActive( true );
            yield return new WaitForSeconds( 3 );
            userInterface.SuccessText.SetActive( false );

            LevelCompletedEventManager.Trigger( levelType );
        }

        public static void ClearLevelData()
        {
            var userInterface = UiItemsRepository.GetUserInterface();
            
            userInterface.TargetsContainer.FindChildren().ForEach( Object.Destroy );

            InteractiveElementRepository.RemoveAll();
        }
    }
}