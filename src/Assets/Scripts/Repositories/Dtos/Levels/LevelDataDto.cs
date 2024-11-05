using System.Linq;
using Assets.Scripts.Application.Elements;
using Assets.Scripts.Application.Levels;

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