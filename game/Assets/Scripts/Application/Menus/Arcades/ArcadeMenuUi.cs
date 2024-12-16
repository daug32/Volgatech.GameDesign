using Assets.Scripts.Application.Menus.Arcades.Levels;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models;
using Assets.Scripts.Application.Menus.Arcades.Levels.Repositories;
using Assets.Scripts.Application.Menus.Arcades.LevelsMenu;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
using UnityEngine;

namespace Assets.Scripts.Application.Menus.Arcades
{
    internal class ArcadeMenuUi
    {
        private readonly GameObject _gameObject;

        private readonly LevelUi _level;
        private readonly LevelsMenuUi _levelsMenuUi;

        public readonly EventManager OnOpenMainMenuEvent = new();
        
        public ArcadeMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject.ThrowIfNull( nameof( gameObject ) );
            
            var childContainer = new GameObjectChildrenContainer( gameObject );

            // Concrete level
            _level = new LevelUi( childContainer.Get( "level" ) );
            _level.OnOpenMainMenuEvent.AddWithCommonPriority( OnOpenMainMenuEvent.Trigger );

            // Levels list menu
            _levelsMenuUi = new LevelsMenuUi( childContainer.Get( "menu_container" ) );
            _levelsMenuUi.OnOpenMainMenuEvent.AddWithCommonPriority( OnOpenMainMenuEvent.Trigger );
            _levelsMenuUi.OnSelectLevelEvent.AddWithCommonPriority( OpenLevel );
        }

        public void SetActive( bool activity )
        {
            _gameObject.SetActive( activity );

            if ( activity )
            {
                LevelDataRepository.Load();
                _levelsMenuUi.SetActive( true );
                _level.UnloadLevel();
            }
            else
            {
                _levelsMenuUi.SetActive( false );
                _level.UnloadLevel();
            }
        }

        private void OpenLevel( LevelType level )
        {
            _levelsMenuUi.SetActive( false );
            _level.LoadLevel( level );
        }
    }
}