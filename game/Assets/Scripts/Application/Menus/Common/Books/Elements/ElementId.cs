using System;

namespace Assets.Scripts.Application.Menus.Common.Books.Elements
{
    internal class ElementId
    {
        public string Value { get; }

        public ElementId( string value )
        {
            if ( String.IsNullOrWhiteSpace( value ) )
            {
                throw new ArgumentException( "Element Id can not be null or empty" );
            }

            if ( value.Contains( '_' ) ) 
            {
                throw new ArgumentException( "Element Id can not have '_' char" );
            }

            Value = value.ToLower();
        }

        public override bool Equals( object obj )
        {
            if ( obj is not ElementId id )
            {
                return false;
            }

            return id.Value == Value;
        }

        public override int GetHashCode() => HashCode.Combine( Value );

        public override string ToString() => Value ?? "null";
    }
}
