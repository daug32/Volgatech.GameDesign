using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Researches
{
    internal static class ResearchesRemover
    {
        public static void Remove( ResearchesMenuUi researchesMenuUi )
        {
            foreach ( var child in researchesMenuUi.ElementsContainer.FindChildren() )
            {
                Object.Destroy( child );
            }
        } 
    }
}