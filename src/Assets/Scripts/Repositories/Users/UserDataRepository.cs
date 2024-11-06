using Assets.Scripts.Application.Users;
using Assets.Scripts.Repositories.Users.Dtos;
using Assets.Scripts.Utils;

namespace Assets.Scripts.Repositories.Users
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
    }
}