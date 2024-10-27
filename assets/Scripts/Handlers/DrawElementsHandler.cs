using System;
using Assets.Scripts.Elements;
using Assets.Scripts.Models.Elements;
using Assets.Scripts.Models.Levels;
using Assets.Scripts.Utils;
using Assets.Settings;
using UnityEngine;

public class DrawElementsHandler : MonoBehaviour
{
    private GameObject _book;
    private ElementsDataRepository _elementsDataRepository;

    // Start is called before the first frame update
    void Start()
    {
        _book = GameObject.Find( "book" );
        if ( _book == null )
        {
            throw new ArgumentException( "Failed to find a book object" );
        }

        _elementsDataRepository = new ElementsDataRepository( LevelType.Level_0 );

        foreach ( var element in ElementsRepository.Elements.Values ) 
        {
            if ( !_elementsDataRepository.Get( element.Id ).IsDiscovered )
            {
                continue;
            }

            GameObject gameObject = element.MapToGameObject();
            gameObject.WithParent( _book );
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
