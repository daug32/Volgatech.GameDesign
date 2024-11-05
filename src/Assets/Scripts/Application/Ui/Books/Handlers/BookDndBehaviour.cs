using Assets.Scripts.Application.Elements.Handlers;
using Assets.Scripts.Repositories.Elements;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Application.Ui.Books.Handlers
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