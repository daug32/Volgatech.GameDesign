using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Elements;
using Assets.Scripts.Application.Levels;

namespace Assets.Scripts.Repositories.Dtos.Levels
{
    internal class LevelDataDto
    {
        public string[] Targets { get; set; }
        public Dictionary<int, LevelObjectiveDto> Objectives { get; set; }

        public LevelData Convert() => new(
            Targets.Select( x => new ElementId( x ) ),
            Objectives.ToDictionary( x => x.Key, x => x.Value.Convert() ) );
    }
}