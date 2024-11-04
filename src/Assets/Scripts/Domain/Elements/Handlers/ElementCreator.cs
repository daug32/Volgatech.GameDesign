using Assets.Scripts.Domain.Elements.Events;
using Assets.Scripts.Domain.Elements.Repositories;
using Assets.Scripts.Domain.Elements.Repositories.ElementsData;

namespace Assets.Scripts.Domain.Elements.Handlers
{
    public static class ElementCreator
    {
        public static void Create( InteractiveElementId firstParentId, InteractiveElementId secondParentId )
        {
            InteractiveElement firstParent = InteractiveElementRepository.Get( firstParentId );
            ElementId firstElementId = firstParent.Element.Id;
            
            InteractiveElement secondParent = InteractiveElementRepository.Get( secondParentId );
            ElementId secondElementId = secondParent.Element.Id;
            
            ElementId newElementId = ElementsDataRepository.GetByParents( firstElementId, secondElementId );
            if ( newElementId == null )
            {
                return;
            }

            var interactiveElement = InteractiveElement.Create(
                ElementsRepository.Get( newElementId ),
                firstParent.RectTransform.sizeDelta );
            interactiveElement.RectTransform.position = firstParent.RectTransform.position;
            InteractiveElementRepository.Add( interactiveElement );

            InteractiveElementRepository.Remove( firstParentId );
            InteractiveElementRepository.Remove( secondParentId );
            
            ElementCreatedEventManager.Trigger( newElementId );
        }
    }
}