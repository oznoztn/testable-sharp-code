using System;
using System.Collections.Generic;
using System.Text;
using Blog.Data;
using Blog.Data.Models;
using Blog.Services;
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

            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            await using (var context = new BlogDbContext(optionsBuilder.Options))
            {
                context.Articles.Add(new Article()
                {
                    Id = 1,
                    UserId = "1",
                    Title = "Test Article 1"
                });

                context.SaveChanges();
            }

            // ACT
            bool result;
            await using (var context = new BlogDbContext(optionsBuilder.Options))
            {
                var articleService = new ArticleService(context);

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
            const string userId = "101";

            var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

            await using (var context = new BlogDbContext(optionsBuilder.Options))
            {
                context.Articles.Add(new Article()
                {
                    Id = 1,
                    UserId = "1",
                    Title = "Test Article 1"
                });

                await context.SaveChangesAsync();
            }

            // ACT
            bool result;
            await using (var context = new BlogDbContext(optionsBuilder.Options))
            {
                var articleService = new ArticleService(context);

                result = await articleService.IsByUser(articleId, userId);
            }

            // ASSERT
            Assert.False(result);
        }
    }
}
