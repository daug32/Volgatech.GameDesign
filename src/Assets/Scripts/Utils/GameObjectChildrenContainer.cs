using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class GameObjectChildrenContainer
    {
        private readonly Dictionary<string, GameObject> _gameObjects;

        public GameObjectChildrenContainer( GameObject parent ) => _gameObjects = parent
           .FindChildren()
           .ToDictionary( x => x.name, x => x );
        
        public GameObject Get( string name ) => _gameObjects[ name ];
    }
}