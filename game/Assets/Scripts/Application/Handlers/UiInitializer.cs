using Assets.Scripts.Application.GameSettings.Sounds;
using UnityEngine;

namespace Assets.Scripts.Application.Handlers
{
    public static class UiInitializer
    {
        public static void Initialize( Behaviour entry )
        {
            entry.gameObject.AddComponent<SoundSourceBehaviour>();
            UserInterface ui = UiItemsRepository.GetUserInterface();
            ui.Menu.OpenMainMenu();
        }
    }
}