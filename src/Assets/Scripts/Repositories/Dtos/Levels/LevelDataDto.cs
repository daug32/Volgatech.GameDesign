using System.Linq;
using Assets.Scripts.Application.Levels;
using Assets.Scripts.Domain.Elements;

namespace Assets.Scripts.Repositories.Dtos.Levels
{
    internal class LevelDataDto
    {
        public string[] Targets { get; set; }

        public LevelData Convert()
        {
            return new LevelData(
                Targets.Select( x => new ElementId( x ) ) );
        }
    }
}