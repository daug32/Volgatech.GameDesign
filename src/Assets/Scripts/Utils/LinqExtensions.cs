using System;
using System.Collections.Generic;

namespace Assets.Scripts.Utils
{
    internal static class LinqExtensions
    {
        public static void ForEach<T>( this IEnumerable<T> collection, Action<T> func )
        {
            foreach ( var item in collection )
            {
                func( item );
            }
        }
    }
}