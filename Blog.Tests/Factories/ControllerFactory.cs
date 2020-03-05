using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Blog.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Tests.Factories
{
    public static class ControllerFactory<TController> where TController : Controller
    {
        /// <summary>
        /// Kontrol için verilen kullanıcı adına sahip yetkili tahsis eder.
        /// Bu kullanıcı yalnızca ClaimTypes.Name tipinde bir claim'e sahiptir.
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="username"></param>
        public static TController WithClaimPrincipal(TController controller, string username)
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
