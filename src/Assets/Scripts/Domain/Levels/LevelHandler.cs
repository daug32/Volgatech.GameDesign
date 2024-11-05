using System;
using System.Collections;
using System.Linq;
using Assets.Scripts.Domain.Elements.Repositories;
using Assets.Scripts.Domain.Elements.Repositories.ElementsData;
using Assets.Scripts.Domain.Ui;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Domain.Levels
{
    internal static class LevelHandler
    {
        public static void Initialize( LevelType levelType )
        {
            ElementsDataRepository.LoadForLevel( levelType );

            var targetElements = ElementsDataRepository
                .GetTargets()
                .Select( ElementsRepository.Get )
                .ToList();
            if ( !targetElements.Any() )
            {
                return;
            }
            
            var targetsContainer = UiItemRepository.GetTargets();
            foreach ( var targetElement in targetElements )
            {
                targetElement.CreateGameObject().WithParent( targetsContainer );
            }
        }

        public static IEnumerator CompleteLevelIfNeeded( LevelType levelType )
        {
            var targetElements = ElementsDataRepository.GetTargets();
            
            var areAllTargetsDiscovered = targetElements.All( x => ElementsDataRepository.Get( x ).IsDiscovered );
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