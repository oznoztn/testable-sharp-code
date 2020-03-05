using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Services;

namespace Blog.Tests.Fakes
{
    public class FakeImageService : IImageService
    {
        public Task UpdateImage(string imageUrl, string destination, int? width = null, int? height = null)
        {
            return Task.FromResult(Task.Delay(1));
        }
    }
}
