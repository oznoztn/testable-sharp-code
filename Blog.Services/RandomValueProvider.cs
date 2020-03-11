using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Services
{
    public class RandomValueProvider : IRandomValueProvider
    {
        private static Random _random;

        public RandomValueProvider(int seed)
        {
            _random = new Random(seed);   
        }

        public int Next(int min, int max) => _random.Next(min, max);
    }
}
