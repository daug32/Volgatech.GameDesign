using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Events;
using Assets.Scripts.Application.Levels.Handlers;
using Assets.Scripts.Application.Ui.Handlers;
using Assets.Scripts.Tests;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Entry : MonoBehaviour
    {
        private void Start()
        {
            #if UNITY_EDITOR
                ElementsValidator.Validate();
            #endif
            
            UiInitializer.Initialize( this );
        }
    }
}