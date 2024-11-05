using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Elements;
using Assets.Scripts.Repositories.Dtos.Events;

namespace Assets.Scripts.Repositories.Elements
{
    internal static class ElementsDataRepository
    {
        private static Dictionary<ElementId, ElementData> _data;

        static ElementsDataRepository()
        {
            LoadData();
            DataLoadedEventManager.Add( LoadData );
        }

        private static void LoadData()
        {
            _data = DataRepository.Get().Elements.ToDictionary(
                x => new ElementId( x.Key ),
                x => x.Value.Convert() );
        }

        public static ElementData Get( ElementId id ) => _data[ id ];

        public static List<ElementId> GetAll() => _data.Keys.ToList();

        public static HashSet<ElementId> GetDiscoveredElements() => _data
           .Where( x => x.Value.IsDiscovered )
           .Select( x => x.Key )
           .ToHashSet();

        public static bool Exists( ElementId id ) => _data.ContainsKey( id );

        public static ElementId GetByParents( ElementId firstParent, ElementId secondParent )
        {
            var dataPair = _data.FirstOrDefault( x =>
                    x.Value.Parents.Contains( firstParent )
                    && x.Value.Parents.Contains( secondParent ) );
            return dataPair.Key;
        }
    }
}