using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Application.Levels.Extensions;
using Assets.Scripts.Application.Ui.Stars;
using Assets.Scripts.Application.Users;
using Assets.Scripts.Repositories.Levels;
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

        private static GameObject CreateLevelGameObject( ArcadeMenuUi arcadeMenu, LevelType levelType, UserLevelData userLevelData )
        {
            GameObject gameObject = Object.Instantiate( arcadeMenu.ExampleLevel ).WithParent( arcadeMenu.LevelsContainer );
            gameObject.name = levelType.ToString().ToLower();

            LevelRating levelRating = userLevelData.IsLevelCompleted 
                ? LevelRating.CompletedLevel(
                    LevelStatistics.FromUserLevelData( userLevelData ),
                    LevelDataRepository.Get( levelType ).Objectives )
                : LevelRating.NotCompletedLevel();
            
            var childrenContainer = new GameObjectChildrenContainer( gameObject );
            SetText( levelType, childrenContainer.Get( "number" ) );
            SetStars( levelRating, childrenContainer.Get( "stars" ) );
            SetOnClick( arcadeMenu, levelType, gameObject );

            gameObject.SetActive( true );
            return gameObject;
        }

        private static void SetOnClick( ArcadeMenuUi arcadeMenu, LevelType levelType, GameObject gameObject )
        {
            gameObject.GetComponent<Button>().onClick.AddListener( () => arcadeMenu.ChooseLevelEventManger.Trigger( levelType ) );
        }

        private static void SetStars( LevelRating levelRating, GameObject gameObject )
        {
            IEnumerable<GameObject> stars = gameObject.FindChildren();
            int currentStar = 0;
            foreach ( var star in stars )
            {
                var imageComponent = star.GetComponent<Image>();
                imageComponent.color = currentStar < levelRating.StarsAchieved ? StarColors.AchievedStar : StarColors.NotAchievedStar;
                currentStar++;
            }
        }

        private static void SetText( LevelType levelType, GameObject gameObject )
        {
            var currentLevel = levelType.ToLevelNumber();
            gameObject.GetComponent<TextMeshProUGUI>().text = currentLevel.ToString();
        }
    }
}