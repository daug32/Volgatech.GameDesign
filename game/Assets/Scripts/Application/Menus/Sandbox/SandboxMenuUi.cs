using Assets.Scripts.Application.Menus.Common.Books;
using Assets.Scripts.Application.Menus.Common.Books.Handlers;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Sandbox
{
    internal class SandboxMenuUi
    {
        public readonly GameObject GameObject;

        public readonly EventManager GetToMainMenuEvenManager = new();
        public readonly Book Book;
        
        public SandboxMenuUi( GameObject gameObject )
        {
            GameObject = gameObject.ThrowIfNull( message: "No game object provided" );

            var childManager = new GameObjectChildrenContainer( gameObject );
            Book = new Book( childManager.Get( "book" ) );
        }

        public void SetActive( bool isActive )
        {
            GameObject.SetActive( isActive );

            if ( isActive )
            {
                ElementsInteractionBlocker.AllowInteractions();
                Book.Load();
            }
            else
            {
                Book.Unload();
            }
        }
    }
}