using System;
using Assets.Scripts.Application.Ui;
using Assets.Scripts.Application.Ui.Books;
using UnityEngine;

namespace Assets.Scripts.Repositories.Ui
{
    internal static class UiItemsRepository
    {
        private static readonly Lazy<Book> _book = new( () => new Book( GameObject.Find( "book" ) ) );
        public static Book GetBook() => _book.Value;
        
        private static readonly Lazy<UserInterface> _userInterfaceLazy = new( () => new UserInterface(
            GameObject.Find( "UI" ),
            GameObject.Find( "level" ) ) );
        public static UserInterface GetUserInterface() => _userInterfaceLazy.Value;
    }
}
