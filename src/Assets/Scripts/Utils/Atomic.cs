namespace Assets.Scripts.Utils
{
    public class Atomic
    {
        private int _value = 0;
        public void Increment() => _value++;
        public int Get() => _value;
    }
}