using Assets.Scripts.Application.Ui.Books.Handlers;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Ui;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Levels.Handlers
{
    internal static class LevelUnloader
    {
        public static void Unload()
        {
            InteractiveElementRepository.RemoveAll();            
            RemoveTargets();
            DrawBookElementsHandler.RemoveAllElements();
        }

        private static void RemoveTargets()
        {
            var userInterface = UiItemsRepository.GetUserInterface();
            userInterface.TargetsContainer.FindChildren().ForEach( Object.Destroy );
        }
    }
}