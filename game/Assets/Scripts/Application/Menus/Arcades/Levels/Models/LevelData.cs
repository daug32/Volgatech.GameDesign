using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Common.Books.Elements;

namespace Assets.Scripts.Application.Menus.Arcades.Levels.Models
{
    internal class LevelData
    {
        public HashSet<ElementId> Targets { get; private set; }
        public List<LevelObjective> Objectives { get; private set; }
        public IReadOnlyCollection<ElementId> StartElements { get; private set; }

        public LevelData(
            IEnumerable<ElementId> targets,
            List<LevelObjective> objectives, 
            HashSet<ElementId> startElements )
        {
            Targets = targets.ToHashSet();
            Objectives = objectives.ToList();
            StartElements = startElements;
        }

        public bool IsLevelCompleted( HashSet<ElementId> discoveredElements )
        {
            foreach ( var target in Targets )
            {
                if ( discoveredElements.Contains( target ) )
                {
                    continue;
                }

                return false;
            }

            return true;
        }
    }
}