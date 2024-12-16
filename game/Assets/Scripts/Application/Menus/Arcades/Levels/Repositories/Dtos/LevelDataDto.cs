using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Assets.Scripts.Application.Menus.Arcades.Repositories.Dtos;
using Assets.Scripts.Application.Menus.Common.Books.Elements;

namespace Assets.Scripts.Application.Menus.Arcades.Levels.Repositories.Dtos
{
    internal class LevelDataDto
    {
        public string[] Targets { get; set; }
        public HashSet<string> StartElements { get; set; }
        public List<LevelObjectiveDto> Objectives { get; set; }

        public LevelData Convert() => new(
            Targets.Select( x => new ElementId( x ) ),
            Objectives.Select( x => x.Convert() ).ToList(),
            StartElements.Select( x => new ElementId( x ) ).ToHashSet() );
    }
}