using System.Collections;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Events;
using Assets.Scripts.Application.Ui;
using Assets.Scripts.Application.Ui.Books.Handlers;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
using Assets.Scripts.Repositories.Ui;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Elements.Handlers
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
            
            RemoveUsedElements( firstParentId, secondParentId );

            yield return DiscoverElementIfNeeded( newElementId );
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

        private static IEnumerator DiscoverElementIfNeeded( ElementId elementId )
        {
            var elementData = ElementsDataRepository.Get( elementId );
            if ( elementData.IsDiscovered )
            {
                yield return null;
            }

            elementData.IsDiscovered = true;
            DrawBookElementsHandler.Draw( elementId );

            UserInterface userInterface = UiItemsRepository.GetUserInterface();

            LevelType currentLevel = userInterface.Level.CurrentLevel.ThrowIfNull( message: "Level was not loaded" );
            LevelData levelData = LevelDataRepository.Get( currentLevel );
            
            if ( levelData.IsLevelCompleted( ElementsDataRepository.GetDiscoveredElements() ) )
            {
                LevelCompletedEventManager.Trigger();
            }
        }
    }
}