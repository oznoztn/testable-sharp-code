namespace Blog.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Models;
    using Services;

    public class ArticlesController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        private readonly int _articlePageSize;

        public ArticlesController(
            IArticleService articleService,
            IMapper mapper,
            IConfiguration configuration)
        {
            this._articleService = articleService;
            this._mapper = mapper;

            _articlePageSize = 
                configuration
                    .GetSection("Articles")
                    .GetValue<int>("PageSize");
        }

        public async Task<IActionResult> All([FromQuery]int page = 1) 
            => this.View(new ArticleListingViewModel
            {
                Articles = await this._articleService.All(page, _articlePageSize),
                Total = await this._articleService.Total(),
                Page = page
            });

        public async Task<IActionResult> Details(int id)
        {
            var article = await this._articleService.Details(id);

            if (article == null)
            {
                return this.NotFound();
            }

            if (!this.User.IsAdministrator() 
                && !article.IsPublic 
                && article.Author != this.User.Identity.Name)
            {
                return this.NotFound();
            }

            return this.View(article);
        }

        public async Task<IActionResult> Random([FromServices]IRandomValueProvider random)
        {
            var ids = (await this._articleService.AllIds()).ToList();

            var randomId = ids[random.Next(0, ids.Count)];

            return await this.Details(randomId);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create() => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ArticleFormModel article)
        {
            if (this.ModelState.IsValid)
            {
                await this._articleService.Add(article.Title, article.Content, this.User.GetId());

                this.TempData.Add(ControllerConstants.SuccessMessage, "Article created successfully it is waiting for approval!");

                return this.RedirectToAction(nameof(this.Mine));
            }

            return this.View(article);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await this._articleService.Details(id);

            if (article == null || (article.Author != this.User.Identity.Name && !this.User.IsAdministrator()))
            {
                return this.NotFound();
            }

            var articleEdit = this._mapper.Map<ArticleFormModel>(article);

            return this.View(articleEdit);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ArticleFormModel article)
        {
            if (!await this._articleService.IsByUser(id, this.User.GetId()) && !this.User.IsAdministrator())
            {
                return this.NotFound();
            }
            
            if (this.ModelState.IsValid)
            {
                await this._articleService.Edit(id, article.Title, article.Content);

                this.TempData.Add(ControllerConstants.SuccessMessage, "Article edited successfully and is waiting for approval!");

                return this.RedirectToAction(nameof(this.Mine));
            }

            return this.View(article);
        }
        
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            if (!await this._articleService.IsByUser(id, this.User.GetId()) && !this.User.IsAdministrator())
            {
                return this.NotFound();
            }

            return this.View(id);
        }
        
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            if (!await this._articleService.IsByUser(id, this.User.GetId()) && !this.User.IsAdministrator())
            {
                return this.NotFound();
            }

            await this._articleService.Delete(id);
            
            this.TempData.Add(ControllerConstants.SuccessMessage, "Article deleted successfully!");

            return this.RedirectToAction(nameof(this.Mine));
        }

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            var articles = await this._articleService.ByUser(this.User.GetId());

            return this.View(articles);
        }
    }
}
