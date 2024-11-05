using Assets.Scripts.Domain.Elements.Handlers;
using Assets.Scripts.Repositories.Elements;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Domain.Book.Handlers
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