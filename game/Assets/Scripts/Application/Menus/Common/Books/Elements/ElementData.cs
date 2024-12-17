using System.Collections.Generic;

namespace Assets.Scripts.Application.Menus.Common.Books.Elements
{
    internal class ElementData
    {
        public int DisplayOrder { get; }
        public List<HashSet<ElementId>> Parents { get; }

        public ElementData( List<HashSet<ElementId>> parents, int displayOrder )
        {
            Parents = parents;
            DisplayOrder = displayOrder;
        }
    }
}
