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

namespace Assets.Scripts.Application.Ui.Arcades.Handlers
{
    internal static class ArcadeMenuDrawer
    {
        public static void Draw( ArcadeMenuUi arcadeMenu )
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
                CreateLevelGameObject( arcadeMenu, levelType, levelData );
            }
        }

        private static GameObject CreateLevelGameObject( ArcadeMenuUi arcadeMenu, LevelType levelType, UserLevelData levelData )
        {
            GameObject gameObject = Object.Instantiate( arcadeMenu.ExampleLevel ).WithParent( arcadeMenu.LevelsContainer );

            gameObject.name = levelType.ToString().ToLower();

            var childrenContainer = new GameObjectChildrenContainer( gameObject );
            
            SetText( levelType, childrenContainer.Get( "number" ) );
            SetStars( levelData, childrenContainer.Get( "stars" ) );
            SetOnClick( arcadeMenu, levelType, gameObject );

            gameObject.SetActive( true );
            
            return gameObject;
        }

        private static void SetOnClick( ArcadeMenuUi arcadeMenu, LevelType levelType, GameObject gameObject )
        {
            gameObject.GetComponent<Button>().onClick.AddListener( () => arcadeMenu.ChooseLevelEventManger.Trigger( levelType ) );
        }

        private static void SetStars( UserLevelData levelData, GameObject gameObject )
        {
            IEnumerable<GameObject> stars = gameObject.FindChildren();
            int competition = LevelCompetitionCalculator.Calculate( levelData.BestCompetitionTime );
            int currentStar = 0;
            foreach ( var star in stars )
            {
                var imageComponent = star.GetComponent<Image>();
                imageComponent.color = CalculateStarColor( currentStar < competition );
                currentStar++;
            }
        }

        private static void SetText( LevelType levelType, GameObject gameObject )
        {
            var currentLevel = levelType.ToLevelNumber() + 1;
            gameObject.GetComponent<TextMeshProUGUI>().text = currentLevel.ToString();
        }

        private static Color CalculateStarColor( bool isStarAchieved ) => isStarAchieved
            ? new Color( 1, 1, 0 )
            : new Color(0.4f, 0.4f, 0 );
    }
}