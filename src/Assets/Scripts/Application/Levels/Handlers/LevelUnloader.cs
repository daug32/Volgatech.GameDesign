using Assets.Scripts.Application.Ui.Books.Handlers;
using Assets.Scripts.Repositories.Elements;

namespace Assets.Scripts.Application.Levels.Handlers
{
    internal static class LevelUnloader
    {
        public static void Unload()
        {
            InteractiveElementRepository.RemoveAll();            
            DrawBookElementsHandler.RemoveAllElements();
        }
    }
}