using Assets.Scripts.Domain.Elements.Handlers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Domain.Book
{
    public class BookDnDHandler : MonoBehaviour, IDropHandler
    {
        public void OnDrop( PointerEventData eventData )
        {
            if ( eventData.pointerDrag == null )
            {
                return;
            }

            ElementDnDBehaviour dndElementBehaviour = eventData.pointerDrag.GetComponent<ElementDnDBehaviour>();
            Destroy( dndElementBehaviour.InteractiveElementGameObject );
        }
    }
}