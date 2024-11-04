using Assets.Scripts.Domain.Ui;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Domain.Elements.Handlers
{
    internal class ElementDnDBehaviour : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
    {
        [SerializeField]
        public RectTransform BookRectTransform;

        public GameObject InteractiveElementGameObject { get; private set; }

        public void OnBeginDrag( PointerEventData eventData )
        {
            var isElementIcon = gameObject != InteractiveElementGameObject; 
            if ( isElementIcon )
            {
                BookRectTransform.ThrowIfNull( nameof( BookRectTransform ) );
                InitializeInteractiveElement();
            }

            var canvasGroup = InteractiveElementGameObject.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag( PointerEventData eventData )
        {
            InteractiveElementGameObject.transform.position = eventData.position;
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            var canvasGroup = InteractiveElementGameObject.GetComponent<CanvasGroup>();
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
            InteractiveElementGameObject = Instantiate( gameObject ).WithParent( UiItemRepository.GetCanvas() );
            
            // Set size to prevent unconscious drag and drop movement 
            RectTransform currentElementRectTransform = gameObject.GetComponent<RectTransform>();
            var interactiveElementRectTransform = InteractiveElementGameObject.GetComponent<RectTransform>();
            interactiveElementRectTransform.sizeDelta = currentElementRectTransform.sizeDelta;

            var dnDBehaviour = interactiveElementRectTransform.GetComponent<ElementDnDBehaviour>();
            // Set InteractiveElement to prevent creating more duplicates
            dnDBehaviour.InteractiveElementGameObject = InteractiveElementGameObject;
            dnDBehaviour.BookRectTransform = BookRectTransform;
        }
    }
}
