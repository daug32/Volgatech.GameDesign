namespace Assets.Scripts.Application.Handlers
{
    public static class UiInitializer
    {
        public static void Initialize()
        {
            UserInterface ui = UiItemsRepository.GetUserInterface();
            ui.Menu.OpenMainMenu();
        }
    }
}