using System.Collections;
using Assets.Scripts.Domain.Book;
using Assets.Scripts.Domain.Elements;
using Assets.Scripts.Domain.Elements.Events;
using Assets.Scripts.Domain.Elements.Repositories.ElementsData;
using Assets.Scripts.Domain.Levels;
using Assets.Scripts.Tests;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Entry : MonoBehaviour
    {
        [SerializeField] 
        public LevelType CurrentLevel = LevelType.Level_0;
        
        private static readonly DrawBookElementsHandler _elementsHandler = new();
            
        private void Start()
        {
            Begin();
            
            #if UNITY_EDITOR
                ElementsDependencyValidator.Validate();
            #endif
        }

        private void Begin()
        {
            LevelHandler.Initialize( CurrentLevel );

            _elementsHandler.DrawAll();
            
            ElementCreatedEventManager.AddWithHighestPriority( OnElementDiscovered );
            ElementCreatedEventManager.AddWithLowestPriority( _ => StartCoroutine( LevelHandler.CompleteLevelIfNeeded( CurrentLevel ) ) );

            LevelCompletedEventManager.Add( _ => SwitchLevel() );
        }

        private void Down()
        {
            ElementCreatedEventManager.RemoveAllListeners();
            LevelCompletedEventManager.RemoveAllListeners();
            LevelHandler.ClearLevelData();
            _elementsHandler.RemoveAllElements();
        }

        private void SwitchLevel()
        {
            Down();
            
            if ( CurrentLevel == LevelType.Level_0 )
            {
                CurrentLevel = LevelType.Level_1;
            }
            else
            {
                CurrentLevel = LevelType.Level_0;
            }
            
            Begin();
        }

        private static void OnElementDiscovered( ElementId elementId )
        {
            var elementData = ElementsDataRepository.Get( elementId );
            if ( elementData.IsDiscovered )
            {
                return;
            }

            elementData.IsDiscovered = true;
            _elementsHandler.Draw( elementId );
        }
    }
}