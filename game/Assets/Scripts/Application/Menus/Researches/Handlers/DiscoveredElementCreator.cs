using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Common.Books.Elements;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Application.Menus.Researches.Handlers
{
    internal static class DiscoveredElementCreator
    {
        public static GameObject Create( ElementId elementId )
        {
            var element = ElementsRepository.Get( elementId );
            var parents = ElementsDataRepository
               .Get( elementId ).Parents
               .FirstOrDefault()
               ?.Select( x => new
                    {
                        ElementId = x,
                        Data = ElementsDataRepository.Get( x ),
                        Element = ElementsRepository.Get( x )
                    } )
               .OrderBy( x => x.Data.DisplayOrder )
               .Select( x => x.Element )
               .ToList() ?? new List<Element>();
            
            var elementGameObject = BuildElementContainer( elementId );
            BuildElementIcon( element.Sprite )
               .WithParent( elementGameObject )
               .WithSize( new Vector2( 30, 30 ) );

            var parentsContainer = BuildParentsContainer().WithParent( elementGameObject );
            foreach ( var parent in parents )
            {
                BuildParent( parent.Sprite )
                   .WithParent( parentsContainer )
                   .WithSize( new Vector2( 20, 20 ) );
            }

            return elementGameObject;
        }

        private static GameObject BuildParent( Sprite sprite )
        {
            var parentIcon = new GameObject( "parent" );
            var image = parentIcon.AddComponent<Image>();
            image.sprite = sprite;
            image.preserveAspect = true;
            image.maskable = false;
            image.raycastTarget = false;
            return parentIcon;
        }

        private static GameObject BuildElementIcon( Sprite sprite )
        {
            var elementIcon = new GameObject( "icon" );
            var image = elementIcon.AddComponent<Image>();
            image.sprite = sprite;
            image.preserveAspect = true;
            image.raycastTarget = false;
            image.maskable = false;
            return elementIcon;
        }

        private static GameObject BuildParentsContainer()
        {
            var parentsContainer = new GameObject( "parents_container" );
            HorizontalLayoutGroup horizontalLayoutGroup = parentsContainer.AddComponent<HorizontalLayoutGroup>();
            horizontalLayoutGroup.spacing = 5;
            horizontalLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
            horizontalLayoutGroup.childControlHeight = false;
            horizontalLayoutGroup.childControlWidth = false;
            ContentSizeFitter contentSizeFilter = parentsContainer.AddComponent<ContentSizeFitter>();
            contentSizeFilter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFilter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            return parentsContainer;
        }

        private static GameObject BuildElementContainer( ElementId elementId )
        {
            var element = new GameObject( $"element_{elementId}" );
            VerticalLayoutGroup verticalLayoutGroup = element.AddComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.childAlignment = TextAnchor.MiddleCenter;
            verticalLayoutGroup.childControlHeight = false;
            verticalLayoutGroup.childControlWidth = false;
            ContentSizeFitter contentSizeFilter = element.AddComponent<ContentSizeFitter>();
            contentSizeFilter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;
            contentSizeFilter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
            return element;
        }
    }
}