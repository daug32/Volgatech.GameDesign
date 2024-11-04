using System;
using Assets.Scripts.Domain.Elements.Repositories;
using Assets.Scripts.Domain.Ui;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Domain.Elements.Handlers
{
    internal static class ElementDnDBehaviourApplier
    {
        public static ElementDnDBehaviour AddDragAndDrop(
            this GameObject obj,
            Element element,
            RectTransform bookRectTransform )
        {
            var dnd = obj.AddComponent<ElementDnDBehaviour>();
            dnd.BookRectTransform = bookRectTransform;
            dnd.Element = element;
            dnd.IsIconElement = true;

            return dnd;
        } 
    }

    internal class ElementDnDBehaviour : 
        MonoBehaviour,
        IPointerDownHandler,
        IBeginDragHandler,
        IEndDragHandler,
        IDragHandler,
        IDropHandler
    {
        [SerializeField]
        public RectTransform BookRectTransform;
        [SerializeField] 
        public Element Element;
        [SerializeField]
        public bool IsIconElement;

        public Guid InteractiveElementId { get; private set; }

        private void Start()
        {
            Element.ThrowIfNull( nameof( Element ) );
            BookRectTransform.ThrowIfNull( nameof( Element ) );
        }

        public void OnBeginDrag( PointerEventData eventData )
        { 
            if ( IsIconElement )
            {
                var interactiveElement = InteractiveElement.Create( Element, gameObject, UiItemRepository.GetCanvas() );
                InteractiveElementRepository.Add( interactiveElement );
                InteractiveElementId = interactiveElement.SceneId;

                ElementDnDBehaviour dnDBehaviour = interactiveElement.DnDBehaviour;
                dnDBehaviour.BookRectTransform = BookRectTransform;
                dnDBehaviour.Element = Element;
                dnDBehaviour.InteractiveElementId = InteractiveElementId;
                dnDBehaviour.IsIconElement = false;
            }

            var element = InteractiveElementRepository.Get( InteractiveElementId );
            var canvasGroup = element.CanvasGroup;
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag( PointerEventData eventData )
        {
            var InteractiveElement = InteractiveElementRepository.Get( InteractiveElementId );
            InteractiveElement.GameObject.transform.position = eventData.position;
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            if ( !InteractiveElementRepository.Exists( InteractiveElementId ) )
            {
                return;
            }
            
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
        }
    }
}
