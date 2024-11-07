using Assets.Scripts.Application.Levels;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Assets.Scripts.Repositories.Dtos.Levels
{
    internal class LevelObjectiveDto
    {
        [JsonConverter( typeof( StringEnumConverter ) )]
        public LevelObjectiveType Type { get; set; }

        public string Data { get; set; }

        public LevelObjective Convert() => new( Type, Data );
    }
}