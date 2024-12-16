namespace Assets.Scripts.Utils.Models.Atomics
{
    public class Atomic
    {
        private int _value = 0;
        public void Increment() => _value++;
        public int Get() => _value;

        public Atomic( int initialValue = 0 )
        {
            _value = initialValue;
        }
    }
}