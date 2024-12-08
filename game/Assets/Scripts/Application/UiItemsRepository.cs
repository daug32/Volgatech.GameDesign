using System;
using UnityEngine;

namespace Assets.Scripts.Application
{
    internal static class UiItemsRepository
    {
        private static readonly Lazy<UserInterface> _userInterfaceLazy = new( () => new UserInterface( GameObject.Find( "UI" ) ) );
        public static UserInterface GetUserInterface() => _userInterfaceLazy.Value;
    }
}
