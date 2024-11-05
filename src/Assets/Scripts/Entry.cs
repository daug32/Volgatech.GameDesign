using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Events;
using Assets.Scripts.Application.Levels.Handlers;
using Assets.Scripts.Tests;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Entry : MonoBehaviour
    {
        [SerializeField] 
        public LevelType CurrentLevel = LevelType.Level_0;
        
        private void Start()
        {
            LevelLoader.Initialize( CurrentLevel );
            LevelCompletedEventManager.Add( () => StartCoroutine( LevelCompleter.Complete() ) );
            
            #if UNITY_EDITOR
                ElementsValidator.Validate();
            #endif
        }
    }
}