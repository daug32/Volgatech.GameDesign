using System;
using Assets.Scripts.Domain.Ui;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Domain.Elements.Handlers
{
    internal class ElementDnDBehaviour : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private bool _isElementIcon = true;
        private GameObject _interactiveElementGameObject;

        public void OnBeginDrag( PointerEventData eventData )
        {
            if ( _isElementIcon )
            {
                _interactiveElementGameObject = CreateInteractiveItem();
            }

            Debug.Log( $"{DateTime.Now}: On begin drag" );
        }

        public void OnDrag( PointerEventData eventData )
        {
            Debug.Log( $"{DateTime.Now}: onDrag" );
            _interactiveElementGameObject.transform.position = eventData.position;
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            Debug.Log( $"{DateTime.Now}: On end drag" );
        }

        public void OnPointerDown( PointerEventData eventData )
        {
            Debug.Log( $"{DateTime.Now}: On pointer down" );
        }

        private GameObject CreateInteractiveItem()
        {
            var interactiveElementGameObject = Instantiate( gameObject ).WithParent( UiItemRepository.GetCanvas() );
            
            // Set size to prevent unconscious drag and drop movement 
            RectTransform currentElementRectTransform = gameObject.GetComponent<RectTransform>();
            RectTransform interactiveElementRectTransform = interactiveElementGameObject.GetComponent<RectTransform>();
            interactiveElementRectTransform.sizeDelta = currentElementRectTransform.sizeDelta;

            // Set InteractiveElement to prevent creating more duplicates
            var dnDBehaviour = interactiveElementRectTransform.GetComponent<ElementDnDBehaviour>();
            dnDBehaviour._interactiveElementGameObject = interactiveElementGameObject;
            dnDBehaviour._isElementIcon = false;

            return interactiveElementGameObject;
        }
    }
}
