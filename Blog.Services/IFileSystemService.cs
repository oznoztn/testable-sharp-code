using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public interface IFileSystemService
    {
        Stream OpenRead(string userImageDestination);
    }
}
