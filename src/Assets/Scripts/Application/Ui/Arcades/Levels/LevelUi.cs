using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Elements;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Ui.Books;
using Assets.Scripts.Application.Ui.Books.Handlers;
using Assets.Scripts.Repositories;
using Assets.Scripts.Repositories.Elements;
using Assets.Scripts.Repositories.Levels;
using Assets.Scripts.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Application.Ui.Arcades.Levels
{
    internal class LevelUi
    {
        private readonly GameObjectChildrenContainer _childrenContainer;

        public LevelType? CurrentLevel { get; private set; }

        private GameObject _gameObject => _childrenContainer.GameObject;
        public readonly Book Book;
        private GameObject _successText => _childrenContainer.Get( "success_text" );
        private GameObject _targetsTitle => _childrenContainer.Get( "targets_title" );
        private GameObject _targetsContainer => _childrenContainer.Get( "targets" );

        public LevelUi( GameObject gameObject )
        {
            _childrenContainer = new GameObjectChildrenContainer( gameObject );
            Book = new Book( _childrenContainer.Get( "book" ) );
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

            // Show targets 
            HashSet<ElementId> targets = LevelDataRepository.Get().Targets;
            if ( targets.Any() )
            {
                _targetsTitle.SetActive( true );
                targets.ForEach( x => ElementsRepository.Get( x ).CreateGameObject().WithParent( _targetsContainer ) );
            }
            else
            {
                _targetsTitle.SetActive( false );
            }

            // Init book
            DrawBookElementsHandler.DrawAll();
            
            CurrentLevel = levelType;
            _gameObject.SetActive( true );
        }

        public void UnloadLevel()
        {
            CurrentLevel = null;

            InteractiveElementRepository.RemoveAll();
            _targetsContainer.FindChildren().ForEach( Object.Destroy );
            DrawBookElementsHandler.RemoveAllElements();

            _gameObject.SetActive( false );
        }
    }
}