using Assets.Scripts.Application.Menus.Common.Books.Elements.Handlers;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Application.Menus.Common.Books.Handlers
{
    public class BookDndBehaviour : MonoBehaviour, IDropHandler
    {
        public void OnDrop( PointerEventData eventData )
        {
            if ( eventData.pointerDrag == null )
            {
                return;
            }

            ElementDndBehaviour dndElementBehaviour = eventData.pointerDrag.GetComponent<ElementDndBehaviour>();
            if ( dndElementBehaviour == null )
            {
                return;
            }

            InteractiveElementRepository.Remove( dndElementBehaviour.InteractiveElementId );
        }
    }
}