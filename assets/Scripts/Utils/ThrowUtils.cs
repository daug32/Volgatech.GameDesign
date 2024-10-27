using System;

namespace Assets.Scripts.Utils
{
    internal static class ThrowUtils
    {
        public static T ThrowIfNull<T>( this T obj, string paramName ) 
        {
            if ( obj == null)
            {
                throw new ArgumentNullException( $"Value can not be null. ParamName: {paramName}" );
            }

            return obj;
        }
    }
}
