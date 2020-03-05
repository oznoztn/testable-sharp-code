using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Blog.Controllers;
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
    }
}
