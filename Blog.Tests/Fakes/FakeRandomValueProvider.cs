using System;
using System.Collections.Generic;
using System.Text;
using Blog.Services;

namespace Blog.Tests.Fakes
{
    public class FakeRandomValueProvider : IRandomValueProvider
    {
        public int NextValue { get; set; }
        public int Next(int min, int max) => NextValue;
    }
}
