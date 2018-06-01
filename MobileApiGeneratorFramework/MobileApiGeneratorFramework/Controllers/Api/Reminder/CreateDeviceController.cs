using System;
using GeneralLibrary.Token;
using GeneralLibrary.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ReminderLibrary.Logic;

namespace MobileApiGeneratorFramework.Controllers.Api.Reminder
{
    [Produces("application/json")]
    [Route("api/CreateDevice")]
    public class CreateDeviceController : Controller
    {
        public ResultViewModel Post( string deviceName, int userId,  string token)
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
            var userMng = new DeviceManager();
            var result = userMng.CreateDevice(deviceName, userId);
            return result;
        }
    }
}