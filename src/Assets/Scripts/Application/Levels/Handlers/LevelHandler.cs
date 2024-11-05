using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Domain.Levels;
using Assets.Scripts.Domain.Levels.Events;
using Assets.Scripts.Repositories;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
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

            var targetsContainer = UiItemRepository.GetTargets();
            foreach ( var targetElement in levelData.Targets )
            {
                ElementsRepository
                   .Get( targetElement )
                   .CreateGameObject()
                   .WithParent( targetsContainer );
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

            var successText = GetSuccessText();

            successText.SetActive( true );
            yield return new WaitForSeconds( 3 );
            successText.SetActive( false );

            LevelCompletedEventManager.Trigger( levelType );
        }

        public static void ClearLevelData()
        {
            var targetsContainer = UiItemRepository.GetTargets();
            foreach ( Transform target in targetsContainer.transform )
            {
                Object.Destroy( target.gameObject );
            }

            InteractiveElementRepository.RemoveAll();
        }

        private static GameObject GetSuccessText()
        {
            foreach ( Transform levelChildTransform in UiItemRepository.GetLevelObject().transform )
            {
                if ( levelChildTransform.gameObject.name != "success_text" )
                {
                    continue;
                }

                return levelChildTransform.gameObject;
            }

            throw new ArgumentException( "Failed to find success text at the scene" );
        }
    }
}