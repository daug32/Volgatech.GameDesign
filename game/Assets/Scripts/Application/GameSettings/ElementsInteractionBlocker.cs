namespace Assets.Scripts.Application.GameSettings
{
    internal static class ElementsInteractionBlocker
    {
        public static bool AreInteractionsBlocked { get; private set; }

        public static void BlockInteractions()
        {
            AreInteractionsBlocked = true;
        }

        public static void AllowInteractions()
        {
            AreInteractionsBlocked = false;
        }
    }
}