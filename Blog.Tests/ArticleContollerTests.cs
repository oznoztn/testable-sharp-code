using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Controllers;
using Blog.Controllers.Models;
using Blog.Services;
using Blog.Services.Infrastructure;
using Blog.Tests.Data;
using Blog.Tests.Fakes;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Blog.Tests
{
    public class ArticleContollerTests
    {
        [Fact]
        public async Task AllShouldReturnModelWithCorrectInformation()
        {
            const string database = "AllShouldReturnModelWithCorrectInformation";
            const int pageSize = 5;
            const int totalArticlesCount = 25;

            await using var context = new FakeDbContext(database);
            await context.SeedArticles(25);

            var mapper = 
                new Mapper(
                    new MapperConfiguration(conf => conf.AddProfile(new ServiceMappingProfile())));
            var dataTimeService = new DateTimeService();
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("Articles:PageSize", pageSize.ToString()), 
                })
                .Build();

            var articleService = new ArticleService(context, mapper, dataTimeService);
            var controller = new ArticlesController(articleService, mapper, configuration);

            ViewResult result = Assert.IsType<ViewResult>(await controller.All());
            ArticleListingViewModel model = Assert.IsType<ArticleListingViewModel>(result.Model);

            Assert.Equal(pageSize, model.Articles.Count());
            Assert.Equal(totalArticlesCount, model.Total);
        }
    }
}
