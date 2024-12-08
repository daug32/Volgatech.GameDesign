using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui.Books
{
    internal class Book
    {
        public readonly GameObject GameObject;
        public readonly RectTransform RectTransform;
        
        public Book( GameObject gameObject )
        {
            GameObject = gameObject.ThrowIfNull( nameof( Book ) );
            RectTransform = gameObject.GetComponent<RectTransform>();
        }
    }
}