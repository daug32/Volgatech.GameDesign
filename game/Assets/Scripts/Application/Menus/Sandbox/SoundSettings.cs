using Assets.Scripts.Utils;

namespace Assets.Scripts.Application.Menus.Sandbox
{
    public static class SoundSettings
    {
        public static bool IsSoundEnabled { get; private set; } = true;
        public static EventManager OnSoundStateChanged { get; } = new();

        public static void SwitchSound()
        {
            IsSoundEnabled = !IsSoundEnabled;
            OnSoundStateChanged.Trigger();
        }
    }
}