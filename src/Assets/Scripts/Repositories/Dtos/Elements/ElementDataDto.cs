using Assets.Scripts.Domain.Elements;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Repositories.Dtos.Elements
{
    internal class ElementDataDto
    {
        public bool? IsDiscovered { get; set; }
        public string[]? Parents { get; set; }

        public ElementData Convert() => new(
            Parents.ThrowIfNull( nameof( Parents ) ),
            isDiscovered: IsDiscovered.ThrowIfNull( nameof( IsDiscovered ) ).Value );
    }
}
