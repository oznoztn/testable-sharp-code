using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blog.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Blog.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public async Task ChangeProfilePictureShouldReturnBadRequestWhenPictureUrlIsNull()
        {
            // arrange
            UsersController usersController = new UsersController();

            // act
            var result = await usersController.ChangeProfilePicture(null);

            // assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ChangeProfilePictureWithNonNullPictureUrlShouldReturnOk()
        {
            // arrange
            const string pictureUrl = "test";
            const string username = "test_user";

            var usersCtrl = new UsersController();

            // REQUIREMENT I : Create a replacement for this.User.Identity
            usersCtrl.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, "test_user")
                    }))
                }
            };

            // act
            var result = await usersCtrl.ChangeProfilePicture(pictureUrl);

            // assert
            Assert.IsType<OkResult>(result);
        }
    }
}
