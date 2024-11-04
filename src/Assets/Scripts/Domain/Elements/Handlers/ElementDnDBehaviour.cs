using Assets.Scripts.Domain.Ui;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Domain.Elements.Handlers
{
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

        public GameObject InteractiveElement { get; private set; }

        public void OnBeginDrag( PointerEventData eventData )
        {
            var isElementIcon = gameObject != InteractiveElement; 
            if ( isElementIcon )
            {
                BookRectTransform.ThrowIfNull( nameof( BookRectTransform ) );
                InitializeInteractiveElement();
            }

            var canvasGroup = InteractiveElement.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag( PointerEventData eventData )
        {
            InteractiveElement.transform.position = eventData.position;
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            var canvasGroup = InteractiveElement.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
        }

        public void OnPointerDown( PointerEventData eventData )
        {
        }

        public void OnDrop( PointerEventData eventData )
        {
        }

        private void InitializeInteractiveElement()
        {
            InteractiveElement = Instantiate( gameObject ).WithParent( UiItemRepository.GetCanvas() );
            
            // Set size to prevent unconscious drag and drop movement 
            RectTransform currentElementRectTransform = gameObject.GetComponent<RectTransform>();
            var interactiveElementRectTransform = InteractiveElement.GetComponent<RectTransform>();
            interactiveElementRectTransform.sizeDelta = currentElementRectTransform.sizeDelta;

            var dnDBehaviour = interactiveElementRectTransform.GetComponent<ElementDnDBehaviour>();
            // Set InteractiveElement to prevent creating more duplicates
            dnDBehaviour.InteractiveElement = InteractiveElement;
            dnDBehaviour.BookRectTransform = BookRectTransform;
        }
    }
}
