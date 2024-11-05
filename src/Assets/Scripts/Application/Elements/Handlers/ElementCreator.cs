using System.Collections;
using System.Linq;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Events;
using Assets.Scripts.Application.Levels.Handlers;
using Assets.Scripts.Application.Ui.Books.Handlers;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
using UnityEngine;

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
            
            LevelData levelData = LevelDataRepository.Get();
            if ( levelData.IsLevelCompleted( ElementsDataRepository.GetDiscoveredElements() ) )
            {
                LevelCompletedEventManager.Trigger();
            }
        }
    }
}