using System;
using Assets.Scripts.Application.Ui;
using UnityEngine;

namespace Assets.Scripts.Repositories.Ui
{
    internal static class UiItemsRepository
    {
        private static readonly Lazy<UserInterface> _userInterfaceLazy = new( () => new UserInterface( GameObject.Find( "UI" ) ) );
        public static UserInterface GetUserInterface() => _userInterfaceLazy.Value;
    }
}
