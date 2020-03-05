using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Tests.Extensions
{
    public static class ControllerExtensions
    {
        /// <summary>
        /// Kontrolün yetkilisini set eder. Kullanıcı yalnızca ClaimTypes.Name tipinde bir claim'e sahip olur.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="username"></param>
        public static TController WithClaimPrincipal<TController>(this TController controller, string username) where TController : Controller
        {
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, username)
                    }))
                }
            };

            return controller;
        }
    }
}
