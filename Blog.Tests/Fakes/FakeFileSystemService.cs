using System.IO;
using Blog.Services;

namespace Blog.Tests.Fakes
{
    public class FakeFileSystemService : IFileSystemService
    {
        public Stream OpenRead(string userImageDestination)
        {
            return Stream.Null;
        }
    }
}