using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Elements;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Repositories.Dtos.Levels;

namespace Assets.Scripts.Repositories.Levels.Dtos
{
    internal class LevelDataDto
    {
        public string[] Targets { get; set; }
        public List<LevelObjectiveDto> Objectives { get; set; }

        public LevelData Convert() => new(
            Targets.Select( x => new ElementId( x ) ),
            Objectives.Select( x => x.Convert() ).ToList() );
    }
}