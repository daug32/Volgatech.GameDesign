using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Domain.Elements
{
    internal class ElementData
    {
        public bool IsDiscovered { get; set; }
        public HashSet<string> Parents { get; private set; }

        public ElementData( IEnumerable<string> parents, bool isDiscovered = false )
        {
            IsDiscovered = isDiscovered;
            Parents = parents.ToHashSet();
        }
    }
}
