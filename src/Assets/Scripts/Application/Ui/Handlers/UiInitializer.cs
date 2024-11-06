using Assets.Scripts.Repositories.Ui;
using UnityEngine;

namespace Assets.Scripts.Application.Ui.Handlers
{
    public static class UiInitializer
    {
        public static void Initialize( MonoBehaviour behaviour )
        {
            UserInterface userInterface = UiItemsRepository.GetUserInterface();

            MainMenuUi mainMenu = userInterface.Menu.MainMenu;
            mainMenu.ArcadeButtonEventManager.AddWithCommonPriority( () => userInterface.ShowArcadeMenu() );
            mainMenu.ExitButtonEventManager.AddWithCommonPriority( UnityEngine.Application.Quit );

            ArcadeMenuUi arcadeMenu = userInterface.Menu.ArcadeMenu;
            arcadeMenu.GetBackButtonEventManager.AddWithCommonPriority( () => userInterface.ShowMainMenu() );

            userInterface.ShowMainMenu();

            // LevelLoader.Initialize( LevelType.Level_0 );
            // LevelCompletedEventManager.Add( () => behaviour.StartCoroutine( LevelCompleter.Complete() ) );
        }
    }
}