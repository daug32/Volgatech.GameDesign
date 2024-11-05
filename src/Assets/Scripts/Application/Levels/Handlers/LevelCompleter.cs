using System.Linq;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
using Assets.Scripts.Repositories.Ui;
using UnityEngine;

namespace Assets.Scripts.Application.Levels.Handlers
{
    internal static class LevelCompleter
    {
        public static void CompleteLevelIfNeeded()
        {
            LevelData levelData = LevelDataRepository.Get();

            var areAllTargetsDiscovered = levelData.Targets.All( x => ElementsDataRepository.Get( x ).IsDiscovered );
            if ( !areAllTargetsDiscovered )
            {
                return;
            }

            var userInterface = UiItemsRepository.GetUserInterface();

            userInterface.SuccessText.SetActive( true );
            var awaiter = new WaitForSeconds( 3 );
            userInterface.SuccessText.SetActive( false );
        }
    }
}