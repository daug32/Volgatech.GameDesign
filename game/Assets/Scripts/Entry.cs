using Assets.Scripts.Application.Handlers;
using Assets.Scripts.Tests;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Entry : MonoBehaviour
    {
        private void Start()
        {
            #if UNITY_EDITOR
                LevelsValidator.Validate();
                ElementsValidator.Validate();
            #endif
            
            UiInitializer.Initialize();
        }
    }
}