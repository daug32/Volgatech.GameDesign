using System.Collections;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Ui.Books;
using Assets.Scripts.Application.Ui.Books.Handlers;
using Assets.Scripts.Repositories;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Ui.Levels
{
    internal class LevelUi
    {
        private readonly GameObjectChildrenContainer _childrenContainer;
        public LevelType? CurrentLevel { get; private set; }

        public readonly Book Book;
        public readonly LevelSettingsUi LevelSettings;
        private GameObject _successText => _childrenContainer.Get( "success_text" );

        public readonly EventManager OpenLevelSettingsEventManager = new();

        public LevelUi( GameObject gameObject )
        {
            _childrenContainer = new GameObjectChildrenContainer( gameObject );
            Book = new Book( _childrenContainer.Get( "book" ) );
            LevelSettings = new LevelSettingsUi( _childrenContainer.Get( "settings" ) );
            _childrenContainer.Get( "settings_button" ).GetComponent<Button>().onClick.AddListener( OpenLevelSettingsEventManager.Trigger );
        }

        public IEnumerator CompleteLevel()
        {
            _successText.SetActive( true );
            yield return new WaitForSeconds( 3 );
            _successText.SetActive( false );
        }

        public void LoadLevel( LevelType levelType )
        {
            DataRepository.LoadForLevel( levelType );

            DrawBookElementsHandler.DrawAll();
            
            CurrentLevel = levelType;
            _childrenContainer.GameObject.SetActive( true );
        }

        public void UnloadLevel()
        {
            CurrentLevel = null;

            InteractiveElementRepository.RemoveAll();
            DrawBookElementsHandler.RemoveAllElements();

            _childrenContainer.GameObject.SetActive( false );
        }
    }
}