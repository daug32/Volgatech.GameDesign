using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Elements;
using Assets.Scripts.Domain.Elements.Repositories;
using Assets.Scripts.Domain.Elements.Repositories.ElementsData;

namespace Assets.Scripts.Tests
{
    public static class ElementsDependencyValidator
    {
        public static void Validate()
        {
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
                foreach ( var parent in data.Parents )
                {
                    elements.Enqueue( parent );
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