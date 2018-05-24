using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Net.Http;

using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using GeneralLibrary.Token;
using Microsoft.AspNetCore.Mvc;
using System;

namespace MobileApiGeneratorFramework.ActionFilters
{
    public class TokenValidatorAttribute : Attribute
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.ActionArguments["Token"].ToString();
          
            var accessTokenManager = new TokenManager();
            // اگر توکن در خواست با مقدار موجود در دیتابیس مطابقت نداشت یا تاریخ مصرف آن به پایان رسیده بود از دسترسی به ای پی آی جلوگیری می شود
            if (accessTokenManager.GetToken(token) == false)
            {
                context.Result = new BadRequestResult
                {
            
                    
                };
                   
            }
            //else
            //{
            //    base.OnActionExecuting(context);
            //}
        }
    }
}
