using System;
using Blog.Controllers;
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
    }
}
