using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneralLibrary.Token;
using GeneralLibrary.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReminderLibrary.Logic;

namespace MobileApiGeneratorFramework.Controllers.Api.Reminder
{
    [Produces("application/json")]
    [Route("api/GetDevisePeriodSchedule")]
    public class GetDevicePeriodScheduleController : Controller
    {
        public ResultViewModel Post(string Id, string token)
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
            var result = dpsMng.GetDevicePeriodScheduls(Id);
            return result;
        }
    }
}