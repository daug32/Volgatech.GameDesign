using Assets.Scripts.Application.Users.Repositories.Dtos;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.Application.Users.Repositories
{
    internal static class UserDataRepository
    {
        private static readonly UserData _data;

        static UserDataRepository()
        {
            _data = JsonHelper
               .Deserialize<UserDataDto>( JsonHelper.LoadJson( $"{Config.UserDataDatabase}/user" ) )
               .Convert();
        }

        public static UserData Get() => _data;

        public static void Commit()
        {
            var json = JsonHelper.Serialize( _data );
            Debug.Log( json );
            // TODO: Add saving
        }
    }
}