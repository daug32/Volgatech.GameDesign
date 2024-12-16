using System;
using System.Globalization;

namespace Assets.Scripts.Application.Menus.Arcades.Levels.Ui.Extensions
{
    internal static class LevelObjectiveExtensions
    {
        public static string ToUserText( this LevelObjective objective )
        {
            switch ( objective.Type )
            {
                case LevelObjectiveType.FinishByReactions: return $"Get by {objective.Data} reactions";
                case LevelObjectiveType.FinishByTime:
                {
                    var time = DateTime.Parse( objective.Data, CultureInfo.InvariantCulture );
                    return $"Get by {time.ToString( "m \"minutes and\" s \"seconds\"" )}";
                }
                default: throw new ArgumentOutOfRangeException( 
                    $"{nameof( objective )}.{nameof( objective.Type )}",
                    $"Unknown objective type. Value: {objective.Type}" );
            }
        }
    }
}