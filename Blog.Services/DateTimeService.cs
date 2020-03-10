using System;

namespace Blog.Services
{
    public class DateTimeService : IDateTimeService
    {
        DateTime IDateTimeService.Now() => DateTime.Now;
        public DateTime UtcNow() => DateTime.UtcNow;
    }
}