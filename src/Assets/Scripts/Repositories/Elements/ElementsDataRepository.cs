using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Domain.Elements;

namespace Assets.Scripts.Repositories.Elements
{
    internal static class ElementsDataRepository
    {
        private static Dictionary<ElementId, ElementData> _data =>
            DataRepository
               .Get().Elements
               .ToDictionary(
                    x => new ElementId( x.Key ),
                    x => x.Value.Convert() );

        public static ElementData Get( ElementId id )
        {
            return _data.ContainsKey( id ) ? _data[ id ] : new ElementData( Array.Empty<string>() );
        }

        public static List<ElementId> GetAll()
        {
            return _data.Keys.ToList();
        }

        public static bool Exists( ElementId id )
        {
            return _data.ContainsKey( id );
        }

        public static ElementId GetByParents( ElementId firstParent, ElementId secondParent )
        {
            var dataPair = _data.FirstOrDefault( x =>
                    x.Value.Parents.Contains( firstParent )
                    && x.Value.Parents.Contains( secondParent ) );
            return dataPair.Key;
        }
    }
}