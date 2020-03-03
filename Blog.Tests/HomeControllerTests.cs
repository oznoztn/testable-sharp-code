using System;
using System.Collections.Generic;
using System.Security.Claims;
using Blog.Controllers;
using Blog.Controllers.Models;
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
            HomeController homeController = new HomeController();

            // ACT
            IActionResult result = homeController.About();

            // ASSERT
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void PrivacyShouldReturnPrivacyViewModelWithCorrectLoggedInUsername()
        {
            // ARRANGE
            HomeController homeController = new HomeController();
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
    }
}
