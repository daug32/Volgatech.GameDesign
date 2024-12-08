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
            RectTransform bookRectTransform )
        {
            var dnd = obj.AddComponent<ElementDndBehaviour>();
            dnd.BookRectTransform = bookRectTransform;
            dnd.Element = element;
            dnd.IsIconElement = true;
            dnd.InteractiveElementId = null;

            return dnd;
        }

        public static ElementDndBehaviour AddInteractiveElementDragAndDrop(
            this GameObject obj,
            Element element,
            RectTransform bookRectTransform,
            InteractiveElementId interactiveElementId )
        {
            var dnd = obj.AddComponent<ElementDndBehaviour>();
            dnd.BookRectTransform = bookRectTransform;
            dnd.Element = element;
            dnd.IsIconElement = false;
            dnd.InteractiveElementId = interactiveElementId;

            return dnd;
        }
    }

    internal class ElementDndBehaviour : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [SerializeField]
        public RectTransform BookRectTransform;
        [SerializeField] 
        public Element Element;
        [SerializeField]
        public bool IsIconElement;

        public InteractiveElementId InteractiveElementId { get; set; }

        private void Start()
        {
            Element.ThrowIfNull( nameof( Element ) );
            BookRectTransform.ThrowIfNull( nameof( Element ) );
        }

        public void OnBeginDrag( PointerEventData eventData )
        { 
            if ( !CanDnd() ) return;

            if ( IsIconElement )
            {
                var interactiveElement = InteractiveElement.Create(
                    Element,
                    gameObject.GetComponent<RectTransform>().sizeDelta );
                InteractiveElementRepository.Add( interactiveElement );
                InteractiveElementId = interactiveElement.SceneId;

                ElementDndBehaviour dndBehaviour = interactiveElement.DndBehaviour;
                dndBehaviour.BookRectTransform = BookRectTransform;
                dndBehaviour.Element = Element;
                dndBehaviour.InteractiveElementId = InteractiveElementId;
                dndBehaviour.IsIconElement = false;
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

            StartCoroutine( ElementCreator.Create( InteractiveElementId, anotherDnd.InteractiveElementId ) );
        }

        private bool CanDnd()
        {
            return !UiItemsRepository.GetUserInterface().Menu.ArcadeMenu.Level.AreInteractionsBlocked;
        }
    }
}
