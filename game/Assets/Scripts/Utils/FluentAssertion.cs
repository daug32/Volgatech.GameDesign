﻿using System;

namespace Assets.Scripts.Utils
{
    internal static class FluentAssertion
    {
        public static T ThrowIfNull<T>( this T? obj, string paramName = null, string message = null )
            where T : struct
        {
            if ( obj.HasValue )
            {
                return obj.Value;
            }

            if ( !String.IsNullOrWhiteSpace( message ) )
            {
                throw new ArgumentException( message );
            }

            if ( !String.IsNullOrWhiteSpace( paramName ) )
            {
                throw new ArgumentNullException( $"Value can not be null. ParamName: {paramName}" );
            }

            throw new ArgumentException( "Value can not be null" );
        }
        
        public static T ThrowIfNull<T>( this T obj, string paramName = null, string message = null )
            where T : class
        {
            if ( obj != null )
            {
                return obj;
            }

            if ( !String.IsNullOrWhiteSpace( message ) )
            {
                throw new ArgumentNullException( message );
            }

            if ( !String.IsNullOrWhiteSpace( paramName ) )
            {
                throw new ArgumentNullException( $"Value can not be null. ParamName: {paramName}" );
            }

            throw new ArgumentException( "Value can not be null" );
        }
    }
}
