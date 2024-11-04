using Assets.Scripts.Domain.Elements.Repositories;
using Assets.Scripts.Domain.Elements.Repositories.ElementsData;
using Assets.Scripts.Domain.Levels;
using Assets.Scripts.Domain.Ui;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Domain.Elements.Handlers
{
    public class DrawElementsHandler : MonoBehaviour
    {
        private ElementsDataRepository _elementsDataRepository;

        // Start is called before the first frame update
        private void Start()
        {
            _elementsDataRepository = new ElementsDataRepository( LevelType.Level_0 );

            foreach ( Element element in ElementsRepository.Elements.Values )
            {
                if ( !_elementsDataRepository.Get( element.Id ).IsDiscovered )
                {
                    continue;
                }

                GameObject elementGameObject = element.CreateGameObject().WithParent( UiItemRepository.GetBook() );
                elementGameObject.AddComponent<ElementDnDBehaviour>();
            }
        }
    }
}