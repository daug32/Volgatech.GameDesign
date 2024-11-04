using Assets.Scripts.Common.Behaviours;
using Assets.Scripts.Domain.Elements.Repositories;
using Assets.Scripts.Domain.Elements.Repositories.ElementsData;
using Assets.Scripts.Domain.Levels;
using Assets.Scripts.Domain.Ui;
using Assets.Scripts.Utils;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Domain.Elements.Handlers
{
    public class DrawElementsHandler : MonoBehaviour
    {
        private ElementsDataRepository _elementsDataRepository;

        // Start is called before the first frame update
        private void Start()
        {
            _elementsDataRepository = new ElementsDataRepository( LevelType.Level_0 );

            foreach ( var element in ElementsRepository.Elements.Values )
            {
                if ( !_elementsDataRepository.Get( element.Id ).IsDiscovered )
                {
                    continue;
                }

                var elementGameObject = RegisterElement( element ).WithParent( UiItemRepository.GetBook() );
                elementGameObject.AddDragAndDrop( UiItemRepository.GetCanvas() );
            }
        }

        private static GameObject RegisterElement( Element element )
        {
            var elementGameObject = new GameObject();

            var elementUiImage = elementGameObject.AddComponent<Image>();
            elementGameObject.name = $"element_{element.Id}";
            elementUiImage.preserveAspect = true;
            elementUiImage.sprite = element.Sprite;
            elementGameObject.SetActive( true );

            return elementGameObject;
        }
    }
}