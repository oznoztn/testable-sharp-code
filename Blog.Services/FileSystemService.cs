using System.IO;

namespace Blog.Services
{
    public class FileSystemService : IFileSystemService
    {
        public Stream OpenRead(string userImageDestination)
        {
            return File.OpenRead(userImageDestination);
        }
    }
}