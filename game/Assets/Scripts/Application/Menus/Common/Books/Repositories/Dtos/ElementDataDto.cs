using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Common.Books.Elements;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Menus.Common.Books.Repositories.Dtos
{
    internal class ElementDataDto
    {
        public bool? IsDiscovered { get; set; }
        public List<string[]> Parents { get; set; }

        public ElementData Convert() => new(
            Parents.Select( parents => parents.Select( parent => new ElementId( parent ) ).ToHashSet() ).ToList(),
            isDiscovered: IsDiscovered.ThrowIfNull( nameof( IsDiscovered ) ) );
    }
}
