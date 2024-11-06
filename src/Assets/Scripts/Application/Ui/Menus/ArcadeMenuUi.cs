using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Handlers;
using Assets.Scripts.Application.Users;
using Assets.Scripts.Repositories.Users;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Application.Ui
{
    internal class ArcadeMenuUi
    {
        private readonly GameObject _gameObject;
        public readonly EventManager GetBackButtonEventManager = new();
        
        private readonly GameObject _levelsContainer;
        private readonly GameObject _exampleLevel;
        
        public ArcadeMenuUi( GameObject gameObject )
        {
            _gameObject = gameObject;
            
            var childContainer = new GameObjectChildrenContainer( gameObject );
            childContainer.Get( "back_button" ).GetComponent<Button>().onClick.AddListener( GetBackButtonEventManager.Trigger );

            _levelsContainer = childContainer.Get( "levels" ); 
            _exampleLevel = _levelsContainer.FindChild( "example_level" );
        }

        public void SetActive( bool activity )
        {
            _gameObject.SetActive( activity );

            if ( activity )
            {
                DrawLevels();
            }
            else
            {
                RemoveLevels();
            }
        }

        private void RemoveLevels()
        {
            foreach ( var level in _levelsContainer.FindChildren() )
            {
                if ( level.name == _exampleLevel.name )
                {
                    continue;
                }
                
                Object.Destroy( level );
            }
        }

        private void DrawLevels()
        {            
            UserData userData = UserDataRepository.Get();

            Dictionary<LevelType, UserLevelData> levels = Enum
               .GetValues( typeof( LevelType ) )
               .Cast<LevelType>()
               .ToDictionary(
                    x => x,
                    x => userData.Arcade.GetValueOrDefault( x, new UserLevelData() ) );

            foreach ( (LevelType levelType, UserLevelData levelData) in levels )
            {
                CreateLevelGameObject( levelType, levelData );
            }
        }

        private GameObject CreateLevelGameObject( LevelType levelType, UserLevelData levelData )
        {
            GameObject gameObject = Object.Instantiate( _exampleLevel ).WithParent( _levelsContainer );

            gameObject.name = levelType.ToString().ToLower();

            var childrenContainer = new GameObjectChildrenContainer( gameObject );
            var levelNumberContainer = childrenContainer.Get( "number" );
            var currentLevel = levelType.ToLevelNumber() + 1;
            levelNumberContainer.GetComponent<TextMeshProUGUI>().text = currentLevel.ToString();

            var levelStarsContainer = childrenContainer.Get( "stars" );
            IEnumerable<GameObject> stars = levelStarsContainer.FindChildren();
            int competition = LevelCompetitionCalculator.Calculate( levelData.BestCompetitionTime );
            int currentStar = 0;
            foreach ( var star in stars )
            {
                var imageComponent = star.GetComponent<Image>();
                imageComponent.color = CalculateStarColor( currentStar < competition );
                currentStar++;
            }

            gameObject.SetActive( true );
            
            return gameObject;
        }

        private Color CalculateStarColor( bool isStarAchieved )
        {
            return isStarAchieved
                ? new Color( 1, 1, 0 )
                : new Color(0.4f, 0.4f, 0 );
        }
    }
}