using Assets.Scripts.Application.Menus.Common.Books;
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
        }
    }
}