using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models.Extensions;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models.Rating;
using Assets.Scripts.Application.Menus.Arcades.Levels.Repositories;
using Assets.Scripts.Application.Menus.Common.Stars;
using Assets.Scripts.Application.Users;
using Assets.Scripts.Application.Users.Repositories;
using Assets.Scripts.Utils;
using Assets.Scripts.Utils.Models.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Application.Menus.Arcades.LevelsMenu.Handlers
{
    internal static class LevelsMenuDrawer
    {
        public static void Draw( LevelsMenuUi levelsMenuUi )
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

                CreateLevelGameObject( levelsMenuUi, levelType, isLevelUnlocked, levelData );
            }
            
            levelsMenuUi.LevelsContainer.SetActive( true );
        }

        private static GameObject CreateLevelGameObject( 
            LevelsMenuUi levelsMenuUi,
            LevelType levelType,
            bool isLevelUnlocked,
            UserLevelData userLevelData )
        {
            GameObject gameObject = Object.Instantiate( levelsMenuUi.ExampleLevel ).WithParent( levelsMenuUi.LevelsContainer );
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
                levelsMenuUi.OnSelectLevelEvent.SubscribeOnClick( gameObject, levelType );
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