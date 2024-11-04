using Assets.Scripts.Domain.Elements.Handlers;
using Assets.Scripts.Domain.Elements.Repositories;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Domain.Book
{
    public class BookDndBehaviour : MonoBehaviour, IDropHandler
    {
        public void OnDrop( PointerEventData eventData )
        {
            if ( eventData.pointerDrag == null )
            {
                return;
            }

            ElementDnDBehaviour dndElementBehaviour = eventData.pointerDrag.GetComponent<ElementDnDBehaviour>();
            InteractiveElementRepository.Remove( dndElementBehaviour.InteractiveElementId );
        }
    }
}