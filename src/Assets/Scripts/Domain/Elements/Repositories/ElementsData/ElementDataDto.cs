using Assets.Scripts.Utils;

namespace Assets.Scripts.Domain.Elements.Repositories.ElementsData
{
    internal class ElementDataDto
    {
        public bool? IsDiscovered { get; set; }
        public string[]? Parents { get; set; }

        public ElementDataDto Apply( ElementDataDto another ) => new ElementDataDto()
        {
            IsDiscovered = another.IsDiscovered == null ? IsDiscovered : another.IsDiscovered,
            Parents = another.Parents == null ? Parents : another.Parents,
        };

        public ElementData Convert() => new ElementData(
            Parents.ThrowIfNull( nameof( Parents ) ),
            IsDiscovered.ThrowIfNull( nameof( IsDiscovered ) ).Value );
    }
}
