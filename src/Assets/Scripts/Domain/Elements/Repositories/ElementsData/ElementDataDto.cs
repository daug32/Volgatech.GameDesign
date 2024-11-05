using Assets.Scripts.Utils;

namespace Assets.Scripts.Domain.Elements.Repositories.ElementsData
{
    internal class ElementDataDto
    {
        public bool? IsDiscovered { get; set; }
        public string[]? Parents { get; set; }
        public bool IsTarget { get; set; }

        public ElementData Convert() => new(
            Parents.ThrowIfNull( nameof( Parents ) ),
            isDiscovered: IsDiscovered.ThrowIfNull( nameof( IsDiscovered ) ).Value,
            isTarget: IsTarget );
    }
}
