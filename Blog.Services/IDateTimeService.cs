using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Services
{
    public interface IDateTimeService
    {
        DateTime Now();
        DateTime UtcNow();
    }
}
