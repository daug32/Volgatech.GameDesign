using Assets.Scripts.Application.Menus.Arcades.Levels.Ui;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Assets.Scripts.Application.Menus.Arcades.Repositories.Dtos
{
    internal class LevelObjectiveDto
    {
        [JsonConverter( typeof( StringEnumConverter ) )]
        public LevelObjectiveType Type { get; set; }

        public string Data { get; set; }

        public LevelObjective Convert() => new( Type, Data );
    }
}