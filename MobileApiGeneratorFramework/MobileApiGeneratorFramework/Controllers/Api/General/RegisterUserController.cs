using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralLibrary.Token;
using GeneralLibrary.User;
using GeneralLibrary.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MobileApiGeneratorFramework.Controllers.Api.General
{
    [Produces("application/json")]
    [Route("api/RegisterUser")]
    public class RegisterUserController : Controller
    {
        [HttpPost]
        public ResultViewModel Post([FromBody]string userName, [FromBody]string password, [FromBody] string token,[FromBody] string name,[FromBody] string family)
        {
            var accessTokenManager = new TokenManager();
            // اگر توکن در خواست با مقدار موجود در دیتابیس مطابقت نداشت یا تاریخ مصرف آن به پایان رسیده بود از دسترسی به ای پی آی جلوگیری می شود
            if (accessTokenManager.GetToken(token) == false)
            {
                return new ResultViewModel
                {
                    Validate = false,
                    ValidateMessage = "Invalid Token",
                    InvalidToken = true
                };
            }
            var userMng = new UserManager();
            var result = userMng.RegisterUser(userName, password,name,family);
            return result;
        }
    }
}