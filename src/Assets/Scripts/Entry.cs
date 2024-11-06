using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Events;
using Assets.Scripts.Application.Levels.Handlers;
using Assets.Scripts.Tests;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Entry : MonoBehaviour
    {
        private void Start()
        {
            LevelLoader.Initialize( LevelType.Level_0 );
            LevelCompletedEventManager.Add( () => StartCoroutine( LevelCompleter.Complete() ) );
            
            #if UNITY_EDITOR
                ElementsValidator.Validate();
            #endif
        }
    }
}