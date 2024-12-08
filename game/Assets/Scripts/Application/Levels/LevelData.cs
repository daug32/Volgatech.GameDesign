using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Elements;

namespace Assets.Scripts.Application.Levels
{
    internal class LevelData
    {
        public HashSet<ElementId> Targets { get; private set; }
        public List<LevelObjective> Objectives { get; private set; }

        public LevelData(
            IEnumerable<ElementId> targets,
            List<LevelObjective> objectives )
        {
            Targets = targets.ToHashSet();
            Objectives = objectives.ToList();
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