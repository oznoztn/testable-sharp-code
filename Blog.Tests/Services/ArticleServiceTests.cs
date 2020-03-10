using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Data;
using Blog.Data.Models;
using Blog.Services;
using Blog.Services.Infrastructure;
using Blog.Tests.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Xunit;

namespace Blog.Tests.Services
{
    public class ArticleServiceTests
    {
        [Fact]
        public async void IsByUserShouldReturnTrueWhenArticleByTheSpecificUserExists()
        {
            // ARRANGE
            const int articleId = 1;
            const string userId = "1";
            const string database = "IsByUserShouldReturnTrueWhenArticleByTheSpecificUserExists";

            await using (var context = new FakeDbContext(database))
            {
                await context.SeedArticles(3);
            }

            // ACT
            bool result;
            await using (var context = new FakeDbContext(database))
            {
                var articleService = new ArticleService(context, null);

                result = await articleService.IsByUser(articleId, userId);
            }

            // ASSERT
            Assert.True(result);
        }

        [Fact]
        public async void IsByUserShouldReturnFalseWhenTheUserDoesntHaveArticleWithGivenArticleId()
        {
            // ARRANGE
            const int articleId = 1;
            const string userId = "10000";
            const string database = "IsByUserShouldReturnFalseWhenTheUserDoesntHaveArticleWithGivenArticleId";

            await using (var context = new FakeDbContext(database))
            {
                await context.SeedArticles(3);
            }

            // ACT
            bool result;
            await using (var context = new FakeDbContext(database))
            {
                var articleService = new ArticleService(context, null);

                result = await articleService.IsByUser(articleId, userId);
            }

            // ASSERT
            Assert.False(result);
        }

        [Fact]
        public async Task AllShouldReturnCorrectNumberOfArticles()
        {
            // arrange
            const string database = "asdfasdfasdfasdfasdfasdfasdf";

            await using var context = new FakeDbContext(database);
            await context.SeedArticles(3);

            // act
            var mapper = new Mapper(new MapperConfiguration(conf =>
            {
                conf.AddProfile<ServiceMappingProfile>();
            }));

            await using var context2 = new FakeDbContext(database);
            var articleService = new ArticleService(context2, mapper);

            var articles = await articleService.All();

            Assert.Equal(3, articles.ToArray().Length);
        }
    }
}
