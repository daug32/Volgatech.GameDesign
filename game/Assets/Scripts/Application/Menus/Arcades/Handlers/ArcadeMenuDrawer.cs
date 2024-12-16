using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Extensions;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Rating;
using Assets.Scripts.Application.Menus.Arcades.Repositories;
using Assets.Scripts.Application.Menus.Common.Stars;
using Assets.Scripts.Application.Users;
using Assets.Scripts.Application.Users.Repositories;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Application.Menus.Arcades.Handlers
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
                LevelType? previousLevel = levelType.GetPreviousLevel();

                bool isLevelUnlocked =
                    // First level is unlocked always
                    levelType == LevelType.Level_0 ||
                    // Unlocked if level was already completed
                    levelData.IsLevelCompleted ||
                    // Unlocked if previous level was completed 
                    ( previousLevel.HasValue && levels[ previousLevel.Value ].IsLevelCompleted );

                CreateLevelGameObject( arcadeMenu, levelType, isLevelUnlocked, levelData );
            }
        }

        private static GameObject CreateLevelGameObject( 
            ArcadeMenuUi arcadeMenu, 
            LevelType levelType,
            bool isLevelUnlocked,
            UserLevelData userLevelData )
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

            if ( isLevelUnlocked )
            {
                SetOnClick( arcadeMenu, levelType, gameObject );
            }
            else 
            {
                ShadeLevel( childrenContainer.Get( "background" ) );
            }

            gameObject.SetActive( true );
            return gameObject;
        }

        private static void ShadeLevel( GameObject background )
        {
            var image = background.GetComponent<Image>();
            Color oldColor = image.color;
            image.color = new Color( oldColor.r, oldColor.g, oldColor.b, 0.6f );
        }

        private static void SetOnClick(
            ArcadeMenuUi arcadeMenu, 
            LevelType levelType, 
            GameObject gameObject )
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