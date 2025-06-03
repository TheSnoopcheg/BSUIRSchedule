using System;
using System.Collections.Generic;

namespace BSUIRSchedule.Services
{
    public interface IDateService
    {
        List<DateTime> GetCurrentWeekDates();
        int GetWeekDiff(DateTime d1, DateTime d2, DayOfWeek startOfWeek = DayOfWeek.Monday);
    }
}
