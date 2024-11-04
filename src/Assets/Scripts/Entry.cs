using Assets.Scripts.Domain.Elements.Events;
using Assets.Scripts.Domain.Elements.Handlers;
using Assets.Scripts.Domain.Elements.Repositories.ElementsData;
using Assets.Scripts.Domain.Levels;
using UnityEngine;

namespace Assets.Scripts
{
    public class Entry : MonoBehaviour
    {
        private static readonly DrawElementsHandler _elementsDrawer = new(); 
            
        private void Start()
        {
            ElementsDataRepository.LoadForLevel( LevelType.Level_0 );

            _elementsDrawer.DrawAll();
            
            ElementCreatedEventManager.AddWithHighestPriority( elementId =>
            {
                ElementsDataRepository.Get( elementId ).IsDiscovered = true;
                _elementsDrawer.Draw( elementId );
            } );
            
            ElementCreatedEventManager.AddWithLowestPriority( elementId => Debug.Log( $"Element created: {elementId}" ) );
        }
    }
}