using System.Linq;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using Assets.Scripts.Application.Users.Repositories;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Menus.Researches.Handlers
{
    internal static class ResearchMenuDrawer
    {
        public static void Draw( ResearchesMenuUi researchesMenu )
        {
            var elements = UserDataRepository
               .Get().DiscoveredElements
               .Select( x => new{ ElementId = x, Data = ElementsDataRepository.Get( x ) } )
               .OrderBy( x => x.Data.DisplayOrder )
               .ToList();
            
            foreach ( var element in elements )
            {
                DiscoveredElementCreator
                   .Create( element.ElementId )
                   .WithParent( researchesMenu.ElementsContainer );
            }
        }
    }
}