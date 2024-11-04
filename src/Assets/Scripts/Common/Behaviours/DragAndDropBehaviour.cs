using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Common.Behaviours
{
    internal class DragAndDropBehaviour : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] 
        [CanBeNull]
        public Canvas Canvas;

        private Lazy<RectTransform> _rectTransformLazy;

        private void Awake()
        {
            _rectTransformLazy = new Lazy<RectTransform>( GetComponent<RectTransform> );
        }

        public void OnBeginDrag( PointerEventData eventData )
        {
            Debug.Log( $"{DateTime.Now}: On begin drag" );
        }

        public void OnDrag( PointerEventData eventData )
        {
            Debug.Log( $"{DateTime.Now}: onDrag" );
            
            Vector2 delta = eventData.delta;
            if ( Canvas != null ) delta /= Canvas.scaleFactor;
            
            _rectTransformLazy.Value.anchoredPosition += delta;
        }

        public void OnEndDrag( PointerEventData eventData )
        {
            Debug.Log( $"{DateTime.Now}: On end drag" );
        }

        public void OnPointerDown( PointerEventData eventData )
        {
            Debug.Log( $"{DateTime.Now}: On pointer down" );
        }
    }
}
