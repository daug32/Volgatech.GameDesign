using Assets.Scripts.Application.Menus.Common.Books.Repositories;

namespace Assets.Scripts.Application.Menus.Common.Books.Elements.Handlers
{
    internal static class ElementCreator
    {
        public static void Create(
            InteractiveElementId firstParentId,
            InteractiveElementId secondParentId,
            Book relatedBook )
        {
            ElementId newElementId = CreateNewElement( firstParentId, secondParentId, relatedBook );
            if ( newElementId == null )
            {
                return;
            }

            relatedBook.OnElementCreatedEvent.Trigger( newElementId );
            
            RemoveUsedElements( firstParentId, secondParentId );
        }

        private static ElementId CreateNewElement(
            InteractiveElementId firstParentId,
            InteractiveElementId secondParentId, 
            Book relatedBook )
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
                firstParent.RectTransform.sizeDelta,
                relatedBook );
            interactiveElement.RectTransform.position = firstParent.RectTransform.position;
            InteractiveElementRepository.Add( interactiveElement );
            
            return newElementId;
        }

        private static void RemoveUsedElements( InteractiveElementId firstParentId, InteractiveElementId secondParentId )
        {
            InteractiveElementRepository.Remove( firstParentId );
            InteractiveElementRepository.Remove( secondParentId );
        }
    }
}