using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Utils.Models.Events
{
    public static class EventManagerExtensions
    {
        public static void SubscribeOnClick( this EventManager eventManager, GameObject gameObject )
        {
            gameObject
               .GetComponent<Button>()
               .ThrowIfNull( message: "Can't subscribe event manager onto a gameObject because it doesn't have button component" )
               .onClick.AddListener( eventManager.Trigger ); 
        } 
        
        public static void SubscribeOnClick<T>( this EventManager<T> eventManager, GameObject gameObject, T data )
        {
            gameObject
               .GetComponent<Button>()
               .ThrowIfNull( message: "Can't subscribe event manager onto a gameObject because it doesn't have button component" )
               .onClick.AddListener( () => eventManager.Trigger( data ) );
        }
    }
}