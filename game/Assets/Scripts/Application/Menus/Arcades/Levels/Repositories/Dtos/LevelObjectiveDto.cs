using Assets.Scripts.Application.Menus.Arcades.Levels.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Assets.Scripts.Application.Menus.Arcades.Levels.Repositories.Dtos
{
    internal class LevelObjectiveDto
    {
        [JsonConverter( typeof( StringEnumConverter ) )]
        public LevelObjectiveType Type { get; set; }

        public string Data { get; set; }

        public LevelObjective Convert() => new( Type, Data );
    }
}