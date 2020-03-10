using System;
using System.Collections.Generic;
using System.Text;
using Blog.Services;

namespace Blog.Tests.Services
{
    public class FakeDateTimeService : IDateTimeService
    {
        public DateTime DateTime { get; set; }
        public DateTime Now()
        {
            return DateTime;
        }

        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
