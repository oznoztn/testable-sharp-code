using System.Threading.Tasks;

namespace Blog.Services
{
    public interface IImageService
    {
        Task UpdateImage(string imageUrl, string destination, int? width = null, int? height = null);
    }
}