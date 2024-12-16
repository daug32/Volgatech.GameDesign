using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Common.Books.Elements;

namespace Assets.Scripts.Application.Menus.Common.Books.Repositories.Dtos
{
    internal class ElementDataDto
    {
        public List<string[]> Parents { get; set; }

        public ElementData Convert() => new(
            Parents.Select( parents => parents.Select( parent => new ElementId( parent ) ).ToHashSet() ).ToList() );
    }
}
