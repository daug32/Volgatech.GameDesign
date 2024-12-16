using System.Collections.Generic;

namespace Assets.Scripts.Application.Menus.Common.Books.Elements
{
    internal class ElementData
    {
        public List<HashSet<ElementId>> Parents { get; private set; }

        public ElementData( List<HashSet<ElementId>> parents )
        {
            Parents = parents;
        }
    }
}
