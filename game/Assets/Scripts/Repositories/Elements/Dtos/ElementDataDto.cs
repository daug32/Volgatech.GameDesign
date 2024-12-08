using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Elements;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Repositories.Elements.Dtos
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
