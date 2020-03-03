namespace Blog.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Services;

    public class HomeController : Controller
    {
        private readonly IArticleService _articleService;

        public HomeController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.All(pageSize: 3);

            return this.View(articles);
        }

        public IActionResult About() => this.View();

        [Authorize]
        public IActionResult Privacy() => this.View(new PrivacyViewModel
        {
            Username = this.User.Identity.Name
        });
    }
}
