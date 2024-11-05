using System;

namespace Assets.Scripts.Domain.Elements
{
    public class InteractiveElementId
    {
        public string Value { get; private set;  } = Guid.NewGuid().ToString();

        public override bool Equals( object obj )
        {
            if ( obj is not InteractiveElementId id )
            {
                return false;
            }

            return id.Value == Value;
        }

        public override int GetHashCode() => HashCode.Combine( Value );

        public override string ToString() => Value;
    }
}