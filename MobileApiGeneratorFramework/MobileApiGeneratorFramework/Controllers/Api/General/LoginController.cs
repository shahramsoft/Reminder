using GeneralLibrary.Token;
using GeneralLibrary.User;
using GeneralLibrary.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace MobileApiGeneratorFramework.Controllers.Api.General
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
       // [TokenValidator]
        [HttpPost]
        public ResultViewModel Post ([FromBody]string userName,[FromBody]string password,[FromBody] string token)
        {
            var accessTokenManager = new TokenManager();
            // اگر توکن در خواست با مقدار موجود در دیتابیس مطابقت نداشت یا تاریخ مصرف آن به پایان رسیده بود از دسترسی به ای پی آی جلوگیری می شود
            if (accessTokenManager.GetToken(token) == false)
            {
                return new ResultViewModel
                {
                    Validate = false,
                    ValidateMessage = "Invalid Token",
                    InvalidToken=true
                };
            }
                var userMng = new UserManager();
            var result = userMng.Login(userName, password);
            return result;
        }
    }
}