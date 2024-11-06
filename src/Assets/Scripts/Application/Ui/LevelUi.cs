using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Application.Ui.Books;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Ui
{
    internal class LevelUi
    {
        private readonly GameObjectChildrenContainer _childrenContainer;

        public GameObject GameObject => _childrenContainer.GameObject;

        public readonly Book Book;
        private GameObject _successText => _childrenContainer.Get( "success_text" );
        private GameObject _targetsTitle => _childrenContainer.Get( "targets_title" );
        private GameObject _targets => _childrenContainer.Get( "targets" );

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

        public void DrawTargets( IEnumerable<GameObject> targets )
        {
            _targets.FindChildren().ForEach( Object.Destroy );
            targets.ForEach( x => x.WithParent( _targets ) );
        }

        public void SetActive( bool activity )
        {
            GameObject.SetActive( activity );
        }
    }
}