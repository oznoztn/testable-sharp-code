using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Blog.Tests.Data
{
    public class FakeDbContext : BlogDbContext
    {
        private readonly string _databaseName;

        public FakeDbContext(string databaseName)
        {
            _databaseName = databaseName;
        }

        public async Task SeedArticles(int count)
        {
            for (int i = 1; i <= count; i++)
            {
                this.Set<Article>().Add(new Article
                {
                    Id = i,
                    Title = $"Title {i}",
                    Content = $"Content {i}",
                    UserId = $"{i}",
                    IsPublic = true
                });
            }

            await this.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_databaseName);
        }
    }
}
