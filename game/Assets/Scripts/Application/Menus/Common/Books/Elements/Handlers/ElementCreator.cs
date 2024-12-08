using System.Collections;
using Assets.Scripts.Application.Menus.Arcades.Levels;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Events;
using Assets.Scripts.Application.Menus.Arcades.Repositories;
using Assets.Scripts.Application.Menus.Common.Books.Handlers;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Menus.Common.Books.Elements.Handlers
{
    public static class ElementCreator
    {
        public static IEnumerator Create( InteractiveElementId firstParentId, InteractiveElementId secondParentId )
        {
            ElementId newElementId = CreateNewElement( firstParentId, secondParentId );
            if ( newElementId == null )
            {
                yield break;
            }

            var level = UiItemsRepository.GetUserInterface().Level;
            level.Statistics.ReactionsNumber.Increment();
            
            RemoveUsedElements( firstParentId, secondParentId );

            yield return DiscoverElementIfNeeded( newElementId, level );
        }

        private static ElementId CreateNewElement( 
            InteractiveElementId firstParentId,
            InteractiveElementId secondParentId )
        {
            InteractiveElement firstParent = InteractiveElementRepository.Get( firstParentId );
            ElementId firstElementId = firstParent.Element.Id;
            
            InteractiveElement secondParent = InteractiveElementRepository.Get( secondParentId );
            ElementId secondElementId = secondParent.Element.Id;
            
            var newElementId = ElementsDataRepository.GetByParents( firstElementId, secondElementId );
            if ( newElementId == null )
            {
                return null;
            }

            var interactiveElement = InteractiveElement.Create(
                ElementsRepository.Get( newElementId ),
                firstParent.RectTransform.sizeDelta );
            interactiveElement.RectTransform.position = firstParent.RectTransform.position;
            InteractiveElementRepository.Add( interactiveElement );
            
            return newElementId;
        }

        private static void RemoveUsedElements( InteractiveElementId firstParentId, InteractiveElementId secondParentId )
        {
            InteractiveElementRepository.Remove( firstParentId );
            InteractiveElementRepository.Remove( secondParentId );
        }

        private static IEnumerator DiscoverElementIfNeeded( ElementId elementId, LevelUi level )
        {
            var elementData = ElementsDataRepository.Get( elementId );
            if ( elementData.IsDiscovered )
            {
                yield return null;
            }

            elementData.IsDiscovered = true;
            DrawBookElementsHandler.Draw( elementId );

            LevelType currentLevel = level.CurrentLevel.ThrowIfNull( message: "Level was not loaded" );
            LevelData levelData = LevelDataRepository.Get( currentLevel );
            
            if ( levelData.IsLevelCompleted( ElementsDataRepository.GetDiscoveredElements() ) )
            {
                LevelCompletedEventManager.Trigger();
            }
        }
    }
}