using System.Collections.Generic;
using Assets.Scripts.Repositories.Dtos.Elements;
using Assets.Scripts.Repositories.Dtos.Levels;

namespace Assets.Scripts.Repositories.Dtos
{
    internal class DataDto
    {
        public LevelDataDto Level { get; set; }
        public Dictionary<string, ElementDataDto> Elements { get; set; }
    }
}