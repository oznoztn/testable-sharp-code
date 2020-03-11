using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Services;
using Blog.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Tests.Fakes
{
    public class FakeArticleService : IArticleService
    {
        readonly List<ArticleListingServiceModel> _data = new List<ArticleListingServiceModel>()
        {
            new ArticleListingServiceModel() {Id = 1, Author = "Author 1", Title = "Title 1"},
            new ArticleListingServiceModel() {Id = 2, Author = "Author 2", Title = "Title 2"},
            new ArticleListingServiceModel() {Id = 3, Author = "Author 3", Title = "Title 3"},
            new ArticleListingServiceModel() {Id = 4, Author = "Author 4", Title = "Title 4"},
            new ArticleListingServiceModel() {Id = 5, Author = "Author 5", Title = "Title 5"},
            new ArticleListingServiceModel() {Id = 6, Author = "Author 6", Title = "Title 6"}
        };

        public async Task<IEnumerable<ArticleListingServiceModel>> All(int page = 1, int pageSize = ServicesConstants.ArticlesPerPage, bool publicOnly = true)
        {
            // Task dönderen bir async bir metodu Task.FromResult ile fake'leyebiliriz:
            return await Task.FromResult(_data.Skip((page - 1) * pageSize).Take(pageSize));
        }

        public Task<IEnumerable<TModel>> All<TModel>(int page = 1, int pageSize = ServicesConstants.ArticlesPerPage, bool publicOnly = true) where TModel : class
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<int>> AllIds()
        {
            return Task.FromResult(_data.Select(t => t.Id));
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
            var model = _data.FirstOrDefault(t => t.Id == id);

            return Task.FromResult(new ArticleDetailsServiceModel()
            {
                Id = model.Id,
                Author = model.Author,
                Title = model.Title,
                PublishedOn = model.PublishedOn,
                IsPublic = true
            });
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
