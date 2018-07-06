using System;
using GeneralLibrary.Token;
using GeneralLibrary.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ReminderLibrary.Logic;

namespace MobileApiGeneratorFramework.Controllers.Api.Reminder
{
    [Produces("application/json")]
    [Route("api/CreateDevisePeriodSchedule")]
    public class CreateDevicePeriodScheduleController : Controller
    {
        public ResultViewModel Post(string deviceId, string userId, string firstDateServise, string serviceTypeName, string periodType, string period, string token)
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
            var dpsMng = new DevicePeriodSheduleManager();
            var result = dpsMng.CreateDevicePeriodSchedule(deviceId, userId, firstDateServise, serviceTypeName, periodType, period);
            return result;
        }
    }
}