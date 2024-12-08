using System.Collections;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Rating;
using Assets.Scripts.Application.Menus.Common.Stars;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.Arcades.Levels
{
    internal class LevelCompletedUi
    {
        private readonly GameObject _gameObject;
        private readonly GameObject _starsContainer;
        
        public LevelCompletedUi( GameObject gameObject )
        {
            _gameObject = gameObject;
            _starsContainer = gameObject.FindChild( "stars" );
        }

        public IEnumerator Show( LevelData levelData, LevelStatistics statistics )
        {
            LevelRating levelRating = LevelRating.CompletedLevel( statistics, levelData.Objectives );
            
            var stars = _starsContainer.FindChildren();
            int currentStar = 0;
            foreach ( var star in stars )
            {
                star.GetComponent<Image>().color = currentStar < levelRating.StarsAchieved ? StarColors.AchievedStar : StarColors.NotAchievedStar;
                currentStar++;
            }
            
            _gameObject.SetActive( true );
            yield return new WaitForSeconds( 3 );
            Hide();
        }

        public void Hide()
        {
            _gameObject.SetActive( false );
        }
    }
}