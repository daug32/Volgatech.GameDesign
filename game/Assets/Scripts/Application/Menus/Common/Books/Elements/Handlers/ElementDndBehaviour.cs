using Assets.Scripts.Application.GameSettings;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Application.Menus.Common.Books.Elements.Handlers
{
    internal static class ElementDndBehaviourApplier
    {
        public static ElementDndBehaviour AddIconDragAndDrop(
            this GameObject obj,
            Element element,
            Book book )
        {
            var dnd = obj.AddComponent<ElementDndBehaviour>();
            dnd.Element = element;
            dnd.IsIconElement = true;
            dnd.InteractiveElementId = null;
            dnd.RelatedBook = book;

            return dnd;
        }

        public static ElementDndBehaviour AddInteractiveElementDragAndDrop(
            this GameObject obj,
            Element element,
            InteractiveElementId interactiveElementId,
            Book book )
        {
            var dnd = obj.AddComponent<ElementDndBehaviour>();
            dnd.Element = element;
            dnd.IsIconElement = false;
            dnd.InteractiveElementId = interactiveElementId;
            dnd.RelatedBook = book;

            return dnd;
        }
    }

    internal class ElementDndBehaviour : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [SerializeField] 
        public Element Element;
        [SerializeField]
        public bool IsIconElement;
        [SerializeField]
        public Book RelatedBook;

        public InteractiveElementId InteractiveElementId { get; set; }

        private void Start()
        {
            Element.ThrowIfNull( nameof( Element ) );
            RelatedBook?.RectTransform.ThrowIfNull( nameof( Element ) );
        }

        public void OnBeginDrag( PointerEventData eventData )
        { 
            if ( !CanDnd() ) return;

            if ( IsIconElement )
            {
                var interactiveElement = InteractiveElement.Create(
                    Element,
                    gameObject.GetComponent<RectTransform>().sizeDelta,
                    RelatedBook );
                InteractiveElementRepository.Add( interactiveElement );
                InteractiveElementId = interactiveElement.Id;

                ElementDndBehaviour dndBehaviour = interactiveElement.DndBehaviour;
                dndBehaviour.Element = Element;
                dndBehaviour.InteractiveElementId = InteractiveElementId;
                dndBehaviour.IsIconElement = false;
                dndBehaviour.RelatedBook = RelatedBook;
            }

            var element = InteractiveElementRepository.Get( InteractiveElementId );
            var canvasGroup = element.CanvasGroup;
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag( PointerEventData eventData )
        {
            if ( !CanDnd() ) return;

            var InteractiveElement = InteractiveElementRepository.Get( InteractiveElementId );
            InteractiveElement.GameObject.transform.position = eventData.position;
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            if ( !CanDnd() ) return;
            if ( !InteractiveElementRepository.Exists( InteractiveElementId ) ) return;
            
            var element = InteractiveElementRepository.Get( InteractiveElementId );
            var canvasGroup = element.CanvasGroup;
            canvasGroup.alpha = 1;
            // Used to pass onDrop element into book dnd handler
            canvasGroup.blocksRaycasts = true;
        }

        public void OnPointerDown( PointerEventData eventData )
        {
        }

        public void OnDrop( PointerEventData eventData )
        {
            if ( !CanDnd() ) return;
            Debug.Log( "OnDrop" );

            if ( eventData.pointerDrag == null )
            {
                return;
            }
            
            if ( IsIconElement )
            {
                return;
            }
            
            var anotherDnd = eventData.pointerDrag.GetComponent<ElementDndBehaviour>();
            if ( anotherDnd == null )
            {
                return;
            }

            ElementCreator.Create(
                InteractiveElementId,
                anotherDnd.InteractiveElementId,
                RelatedBook );
        }

        private bool CanDnd()
        {
            return !ElementsInteractionBlocker.AreInteractionsBlocked;
        }
    }
}
