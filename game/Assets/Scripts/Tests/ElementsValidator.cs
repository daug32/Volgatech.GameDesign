using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Arcades.Levels.Models;
using Assets.Scripts.Application.Menus.Arcades.Levels.Repositories;
using Assets.Scripts.Application.Menus.Common.Books.Elements;
using Assets.Scripts.Application.Menus.Common.Books.Repositories;

namespace Assets.Scripts.Tests
{
    public static class ElementsValidator
    {
        public static void Validate()
        {
            foreach ( LevelType levelType in Enum.GetValues( typeof( LevelType ) ) )
            {
                ValidateForLevel( levelType );
            }
        }

        private static void ValidateForLevel( LevelType levelType )
        {
            LevelDataRepository.Load();
            ElementsDataRepository.LoadForLevel( levelType );
            
            var elementsWithoutSprite = new HashSet<ElementId>();
            var elementsWithoutData = new HashSet<ElementId>();

            var elements = new Queue<ElementId>(
                ElementsDataRepository
                   .GetAll()
                   .Union( ElementsRepository.GetAll().Select( x => x.Id ) ) );
            while ( elements.Any() )
            {
                var elementId = elements.Dequeue();
                
                if ( !ElementsRepository.Exists( elementId ) )
                {
                    elementsWithoutSprite.Add( elementId );
                    continue;
                }

                if ( !ElementsDataRepository.Exists( elementId ) )
                {
                    elementsWithoutData.Add( elementId );
                    continue;
                }

                ElementData data = ElementsDataRepository.Get( elementId );
                foreach ( var parents in data.Parents )
                {
                    foreach ( var parent in parents )
                    {
                        elements.Enqueue( parent );
                    }
                }
            }

            string message = "";
            if ( elementsWithoutData.Any() )
            {
                message += $"\"elementsWithoutData\": [{String.Join( ", ", elementsWithoutData.Select( x => $"\"{x}\"") )}]";
            }

            if ( elementsWithoutSprite.Any() )
            {
                message += $"\"elementsWithoutSprite\": [{String.Join( ", ", elementsWithoutSprite.Select( x => $"\"{x}\"" ) )}]";
            }

            if ( !String.IsNullOrWhiteSpace( message ) )
            {
                throw new ArgumentException( $"Startup validation error. Message: {{ {message} }}" );
            }
        }
    }
}