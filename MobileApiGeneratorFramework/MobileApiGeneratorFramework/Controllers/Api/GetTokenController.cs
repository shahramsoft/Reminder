using GeneralLibrary.Token;
using GeneralLibrary.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace MobileApiGeneratorFramework.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/GetToken")]
    public class GetTokenController : Controller
    {
        [HttpPost]
        public ResultViewModel Token (string clientId,string clientSecret)
        {
            var tokenMng = new TokenManager();
            return tokenMng.ValidateAndReturnToken(clientId,clientSecret);
          
        }
    }
}