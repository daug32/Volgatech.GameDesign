using System.Collections.Generic;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Extensions;
using Assets.Scripts.Application.Menus.Arcades.Repositories;
using Assets.Scripts.Application.Menus.Common.Books.Elements;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using Assets.Scripts.Application.Menus.Common.Stars;
using Assets.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Application.Menus.Arcades.Levels
{
    internal class LevelSettingsUi
    {
        private readonly GameObject _gameObject;

        private readonly GameObject _levelTitle;
        private readonly GameObject _targetsContainer;
        private readonly GameObjectChildrenContainer _starsContainer;

        public readonly EventManager OnGetToMainMenuEventManager = new();
        public readonly EventManager OnRestartLevelEventManager = new();
        public readonly EventManager OnGetToLevelEventManager = new();

		public LevelSettingsUi( GameObject gameObject )
		{
			_gameObject = gameObject;
            
			var childrenContainer = new GameObjectChildrenContainer( gameObject );
			
			_starsContainer = new GameObjectChildrenContainer( childrenContainer.Get( "stars_container" ) );
			
			var targetsContainer = new GameObjectChildrenContainer( childrenContainer.Get( "targets_container" ) );
			_levelTitle = targetsContainer.Get( "title" );
			_targetsContainer = targetsContainer.Get( "items" );
			
			var buttonsContainer = new GameObjectChildrenContainer( childrenContainer.Get( "buttons_container" ) );
			buttonsContainer.Get( "exit" ).GetComponent<Button>().onClick.AddListener( OnGetToMainMenuEventManager.Trigger );
			buttonsContainer.Get( "restart" ).GetComponent<Button>().onClick.AddListener( OnRestartLevelEventManager.Trigger );
			buttonsContainer.Get( "continue" ).GetComponent<Button>().onClick.AddListener( OnGetToLevelEventManager.Trigger );
		}

		public void Hide()
		{
			_gameObject.SetActive( false );
		}

		public void Show( LevelType levelType )
		{
			LevelData levelData = LevelDataRepository.Get( levelType );

			UpdateLevelTitle( levelType );
			DrawTargets( levelData.Targets );
			DrawStars( levelData.Objectives );

			_gameObject.SetActive( true );
		}

		private void UpdateLevelTitle( LevelType levelType )
		{
			_levelTitle.GetComponent<TextMeshProUGUI>().text = $"Level {levelType.ToLevelNumber()}. Targets: ";
		}

		private void DrawStars( List<LevelObjective> levelObjectives )
		{
			List<GameObject> stars = _starsContainer.GetAll();
			for ( var i = 0; i < stars.Count; i++ )
			{
				LevelObjective objective = levelObjectives[ i ];

				GameObject star = stars[ i ];
				var childrenContainer = new GameObjectChildrenContainer( star );

				var image = childrenContainer.Get( "star" ).GetComponent<Image>();
				image.color = StarColors.AchievedStar;
				
				TextMeshProUGUI text = childrenContainer.Get( "text" ).GetComponent<TextMeshProUGUI>();
				text.text = objective.ToUserText();
			}
		}

		private void DrawTargets( HashSet<ElementId> targets )
		{
			_targetsContainer.FindChildren().ForEach( Object.Destroy );

			// Draw new targets
			targets.ForEach( target =>
			{
				var element = ElementsRepository.Get( target );
				var gameObject = element
				   .CreateGameObject()
				   .WithParent( _targetsContainer );
				gameObject.name = $"target_{element.BuildName()}";
			} );
		}
    }
}