using System.Collections.Generic;

namespace Assets.Scripts.Application.Elements
{
    internal class ElementData
    {
        public bool IsDiscovered { get; set; }
        public List<HashSet<ElementId>> Parents { get; private set; }

        public ElementData( 
            List<HashSet<ElementId>> parents,
            bool isDiscovered = false )
        {
            Parents = parents;
            IsDiscovered = isDiscovered;
        }
    }
}
