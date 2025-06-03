using System;

namespace BSUIRSchedule.Services
{
    public static class EventService
    {
        public static event Action? ColorsUpdated;
        public static void ColorsUpdated_Invoke()
        {
            ColorsUpdated?.Invoke();
        }
    }
}
