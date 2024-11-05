using Assets.Scripts.Application.Elements;
using Assets.Scripts.Application.Ui;
using Assets.Scripts.Application.Ui.Books.Handlers;
using Assets.Scripts.Repositories;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
using Assets.Scripts.Repositories.Ui;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Levels.Handlers
{
    internal static class LevelLoader
    {
        public static void Initialize( LevelType levelType, MonoBehaviour behaviour )
        {
            DataRepository.LoadForLevel( levelType );
            
            LevelData levelData = LevelDataRepository.Get();
            UserInterface userInterface = UiItemsRepository.GetUserInterface();
            DrawTargets( levelData, userInterface );
            
            DrawBookElementsHandler.DrawAll();
        }

        private static void DrawTargets( LevelData levelData, UserInterface userInterface )
        {
            foreach ( ElementId targetElement in levelData.Targets )
            {
                ElementsRepository
                   .Get( targetElement )
                   .CreateGameObject()
                   .WithParent( userInterface.TargetsContainer );
            }
        }
    }
}