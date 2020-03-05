using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Services;
using Blog.Services.Models;

namespace Blog.Tests.Fakes
{
    public class FakeArticleService : IArticleService
    {
        public async Task<IEnumerable<ArticleListingServiceModel>> All(int page = 1, int pageSize = ServicesConstants.ArticlesPerPage, bool publicOnly = true)
        {
            var result = new List<ArticleListingServiceModel>()
            {
                new ArticleListingServiceModel() { Id = 1 },
                new ArticleListingServiceModel() { Id = 2 },
                new ArticleListingServiceModel() { Id = 3 },
                new ArticleListingServiceModel() { Id = 4 },
                new ArticleListingServiceModel() { Id = 5 },
                new ArticleListingServiceModel() { Id = 6 }
            };

            // Task dönderen bir async bir metodu Task.FromResult ile fake'leyebiliriz:
            return await Task.FromResult(result.Skip((page - 1) * pageSize).Take(pageSize));
        }

        public Task<IEnumerable<TModel>> All<TModel>(int page = 1, int pageSize = ServicesConstants.ArticlesPerPage, bool publicOnly = true) where TModel : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<int>> AllIds()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ArticleForUserListingServiceModel>> ByUser(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsByUser(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleDetailsServiceModel> Details(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Total()
        {
            throw new NotImplementedException();
        }

        public Task<int> Add(string title, string content, string userId)
        {
            throw new NotImplementedException();
        }

        public Task Edit(int id, string title, string content)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task ChangeVisibility(int id)
        {
            throw new NotImplementedException();
        }
    }
}
