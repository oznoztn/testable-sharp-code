namespace Blog.Services
{
    using System;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ArticleService : IArticleService
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTimeService _dateTimeService;

        public ArticleService(BlogDbContext context, IMapper mapper, IDateTimeService dateTimeService)
        {
            _context = context;
            _mapper = mapper;
            _dateTimeService = dateTimeService;
        }

        public async Task<IEnumerable<ArticleListingServiceModel>> All(
            int page = 1,
            int pageSize = ServicesConstants.ArticlesPerPage,
            bool publicOnly = true)
            => await this.All<ArticleListingServiceModel>(page, pageSize, publicOnly);

        public async Task<IEnumerable<TModel>> All<TModel>(
            int page = 1,
            int pageSize = ServicesConstants.ArticlesPerPage,
            bool publicOnly = true)
            where TModel : class
        {
            var query = this._context.Articles.AsQueryable();

            if (publicOnly)
            {
                query = query.Where(a => a.IsPublic);
            }

            return await query
                .OrderByDescending(a => a.PublishedOn)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ProjectTo<TModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<int>> AllIds()
            => await this._context
                .Articles
                .Where(a => a.IsPublic)
                .Select(a => a.Id)
                .ToListAsync();

        public async Task<IEnumerable<ArticleForUserListingServiceModel>> ByUser(string userId)
            => await this._context
                .Articles
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.PublishedOn)
                .ProjectTo<ArticleForUserListingServiceModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

        /// <summary>
        /// Verilen Id'ye karşılık gelen makalenin, id'si verilen kullanıcıya ait olup olmadığını dönderir.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IsByUser(int id, string userId)
            => await this._context
                .Articles
                .AnyAsync(a => a.Id == id && a.UserId == userId);

        public async Task<ArticleDetailsServiceModel> Details(int id)
            => await this._context
                .Articles
                .Where(a => a.Id == id)
                .ProjectTo<ArticleDetailsServiceModel>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<int> Total()
            => await this._context
                .Articles
                .Where(a => a.IsPublic)
                .CountAsync();

        public async Task<int> Add(string title, string content, string userId)
        {
            var article = new Article
            {
                Title = title,
                Content = content,
                UserId = userId
            };

            this._context.Articles.Add(article);

            await this._context.SaveChangesAsync();

            return article.Id;
        }

        public async Task Edit(int id, string title, string content)
        {
            var article = await this._context.Articles.FindAsync(id);

            if (article == null)
            {
                return;
            }

            article.Title = title;
            article.Content = content;
            article.IsPublic = false;

            await this._context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var article = await this._context.Articles.FindAsync(id);
            this._context.Articles.Remove(article);

            await this._context.SaveChangesAsync();
        }

        public async Task ChangeVisibility(int id)
        {
            var article = await this._context.Articles.FindAsync(id);

            if (article == null)
            {
                return;
            }

            article.IsPublic = !article.IsPublic;

            if (article.PublishedOn == null)
            {
                article.PublishedOn = _dateTimeService.Now();
            }

            await this._context.SaveChangesAsync();
        }
    }
}
