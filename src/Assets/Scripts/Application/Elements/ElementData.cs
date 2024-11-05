using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Application.Elements
{
    internal class ElementData
    {
        public bool IsDiscovered { get; set; }
        public HashSet<ElementId> Parents { get; private set; }

        public ElementData( 
            IEnumerable<string> parents,
            bool isDiscovered = false )
        {
            Parents = parents.Select( x => new ElementId( x ) ).ToHashSet();
            IsDiscovered = isDiscovered;
        }
    }
}
