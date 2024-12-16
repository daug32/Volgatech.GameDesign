using Assets.Scripts.Utils.Models.Events;

namespace Assets.Scripts.Application.GameSettings
{
    public static class SoundSettings
    {
        public static bool IsSoundEnabled { get; private set; } = true;
        public static EventManager OnSoundStateChangedEvent { get; } = new();

        public static void SwitchSound()
        {
            IsSoundEnabled = !IsSoundEnabled;
            OnSoundStateChangedEvent.Trigger();
        }
    }
}