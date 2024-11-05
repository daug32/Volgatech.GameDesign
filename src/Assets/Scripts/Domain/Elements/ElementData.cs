using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Domain.Elements
{
    internal class ElementData
    {
        public bool IsDiscovered { get; set; }
        public bool IsTarget { get; set; }
        public HashSet<ElementId> Parents { get; private set; }

        public ElementData( 
            IEnumerable<string> parents,
            bool isDiscovered = false,
            bool isTarget = false )
        {
            Parents = parents.Select( x => new ElementId( x ) ).ToHashSet();
            IsDiscovered = isDiscovered;
            IsTarget = isTarget;
        }
    }
}
