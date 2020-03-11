using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Services
{
    public interface IRandomValueProvider
    {
        int Next(int min, int max);
    }
}
