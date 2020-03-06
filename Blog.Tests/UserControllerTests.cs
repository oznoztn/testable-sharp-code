using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Blog.Controllers;
using Blog.Services;
using Blog.Tests.Extensions;
using Blog.Tests.Fakes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Blog.Tests
{
    public class UserControllerTests
    {
        /// <summary>
        /// Test username is "TESTUSER".
        /// </summary>
        public const string TestUser = "TESTUSER";
        [Fact]
        public async Task ChangeProfilePictureShouldReturnBadRequestWhenPictureUrlIsNull()
        {
            // arrange
            UsersController usersController = new UsersController(new FakeImageService(), null);

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

            var usersCtrl = 
                new UsersController(new FakeImageService(), null)
                    .WithClaimPrincipal(username);
            
            // act
            var result = await usersCtrl.ChangeProfilePicture(pictureUrl);

            // assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetProfilePictureShouldReturnFileStreamResult()
        {
            // ARRANGE
            var fakeFileSystemService = new FakeFileSystemService();
            var usersController =
                new UsersController(null, fakeFileSystemService)
                    .WithClaimPrincipal(TestUser);
            // ACT

            IActionResult result = await usersController.GetProfilePicture();

            // ASSERT
            Assert.IsType<FileStreamResult>(result);
        }
    }
}
