using System.Linq;
using Assets.Scripts.Application.Ui;
using Assets.Scripts.Application.Ui.Books.Handlers;
using Assets.Scripts.Repositories;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
using Assets.Scripts.Repositories.Ui;

namespace Assets.Scripts.Application.Levels.Handlers
{
    internal static class LevelLoader
    {
        public static LevelType CurrentLevel { get; private set; }
        
        public static void Initialize( LevelType levelType )
        {
            DataRepository.LoadForLevel( levelType );
            
            LevelData levelData = LevelDataRepository.Get();
            UserInterface userInterface = UiItemsRepository.GetUserInterface();
            userInterface.Level.DrawTargets( levelData.Targets.Select( x => ElementsRepository.Get( x ).CreateGameObject() ) );

            DrawBookElementsHandler.DrawAll();

            CurrentLevel = levelType;
        }
    }
}