using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blog.Controllers;
using Blog.Controllers.Models;
using Blog.Services.Models;
using Blog.Tests.Fakes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Blog.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void AboutShouldReturnViewResult()
        {
            // ARRANGE
            HomeController homeController = new HomeController(null);

            // ACT
            IActionResult result = homeController.About();

            // ASSERT
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PrivacyShouldReturnPrivacyViewModelWithCorrectLoggedInUsername()
        {
            // ARRANGE
            HomeController homeController = new HomeController(null);
            homeController.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, "test_user")
                    }))
                }
            };

            // ACT
            IActionResult result = homeController.Privacy();

            // ASSERT
            var viewResult = Assert.IsType<ViewResult>(result);
            var privacyViewModel = Assert.IsType<PrivacyViewModel>(viewResult.Model);
            Assert.Equal("test_user", privacyViewModel.Username);
        }

        // Artýk void döndermiyor dikkat et.
        // Çünkü içeride await keyword'ünü kullanmamýz gerekiyor.
        // Test metodunu signatürü de buna göre düzenleniyor.
        // xUnit bunu destekliyor, diðerlerini bilmem.
        [Fact]
        public async Task IndexShouldReturnViewResultWithCorrectViewModel()
        {
            // ACT
            var homeController = new HomeController(new FakeArticleService());

            // ARRANGE
            // Asenkron olan bir metodu test ettiðimize dikkat et:
            var result = await homeController.Index();

            // ASSERT
            var viewResult = Assert.IsType<ViewResult>(result);

            // Because of the IEnumerable<T>
            //  Do not use Assert.IsType<IEnumerable<ArticleListingServiceModel>>, 
            //    (A list of item instead would be OK)
            var model = Assert.IsAssignableFrom<IEnumerable<ArticleListingServiceModel>>(viewResult.Model);

            Assert.Equal(3, model.ToList().Count);
        }
    }
}
