using GeneralLibrary.ViewModel;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace GeneralLibrary.Token
{
    public class TokenManager: ConnectionManager 
    {
        //============================تولید توکن نسخه جدید===========================
        /// <summary>
        /// The _alg.
        /// </summary>
        private const string _alg = "HmacSHA256";

        /// <summary>
        /// The _salt.
        /// </summary>
        private const string _salt = "rz8LuOtFBXphj9WQfvFh"; // Generated at https://www.random.org/strings

        /// <summary>
        /// The _expiration minutes.
        /// </summary>
        private const int _expirationMinutes = 10;
        public TokenManager():base()
        {

        }
        public bool GetToken(string token)
        {
            try
            {
                var isvalidData = false;
                
                var commandnitgen = @"select * from Tokens where Token='" + token + "'";
                var conn = new SqlConnection(ConnectionString);

                using (var cmd = new SqlCommand(commandnitgen, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                       // int.TryParse(reader["Id"].ToString(), out authenticateId);
                        isvalidData = true;
                    }
                    conn.Close();
                    conn.Dispose();
                    reader.Close();
                }
                return isvalidData;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public ResultViewModel ValidateAndReturnToken (string clientId,string clientSecret)
        {
            try
            {
                var isvalidData = false;
                var authenticateId = 0;
                var commandnitgen = @"select * from AuthenticationApi where ClientId='"+ clientId + "' and ClientSecret='"+ clientSecret + "'";
                var conn = new SqlConnection(ConnectionString);

                using (var cmd = new SqlCommand(commandnitgen, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        int.TryParse(reader["Id"].ToString(), out authenticateId);
                        isvalidData =true;
                    }
                    conn.Close();
                    conn.Dispose();
                    reader.Close();
                }
                if (isvalidData)
                {
                    var resultToken = GenerateToken(clientId, clientSecret, "192.168.0.1", "agent", 250000, DateTime.Now.ToString());
                    commandnitgen = @"insert into Tokens (Token,CreateDate,[ExpireDate],AuthenticationId) values ('"+ resultToken + "','" + DateTime.Now.ToString() + "','"+ DateTime.Now.AddDays(2) + "',"+ authenticateId + ")";
                     conn = new SqlConnection(ConnectionString);

                    using (var cmd = new SqlCommand(commandnitgen, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        var reader = cmd.ExecuteNonQuery();
                        conn.Close();
                        conn.Dispose();
                        
                    }
                    return new ResultViewModel
                    {
                        Validate = true,
                        Message = resultToken
                    };
                }
                else
                {
                    return new ResultViewModel
                    {
                        Validate = false,
                        Message = "",
                        ValidateMessage = "Worng Credential"
                      
                    };
                }
            }
            catch (Exception ex)
            {

                return new ResultViewModel
                {
                    Validate = false,
                    Message = "",
                    ValidateMessage = "عملیات با خطا مواجه شد",
                    ExceptionMessage = ex.Message
                };
            }
        }
        /// <summary>
        /// The generate token.
        /// </summary>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <param name="ip">
        /// The ip.
        /// </param>
        /// <param name="userAgent">
        /// The user agent.
        /// </param>
        /// <param name="ticks">
        /// The ticks.
        /// </param>
        /// <param name="datetime">
        /// The datetime.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GenerateToken(string username, string password, string ip, string userAgent, long ticks, string datetime)
        {
            //datetime = datetime.Replace(':', '/');
            //string hash = string.Join(":", new string[] { username, ip, userAgent, ticks.ToString(), datetime });
            //string hashLeft = "";
            //string hashRight = "";

            //using (KeyedHashAlgorithm hmac = KeyedHashAlgorithm.Create(_alg))
            //{
            //    hmac.Key = Encoding.UTF8.GetBytes(GetHashedPassword(password));
            //    hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));

            //    hashLeft = Convert.ToBase64String(hmac.Hash);
            //    hashRight = string.Join(":", new string[] { username, ticks.ToString(), datetime });
            //}

            //return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
            return GetHash(username, password);
        }
        public static String GetHash(String text, String key)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        public string GetHashedPassword(string password)
        {
            string key = string.Join(":", new string[] { password, _salt });

            using (HMAC hmac = HMACSHA256.Create(_alg))
            {
                // Hash the key.
                hmac.Key = Encoding.UTF8.GetBytes(_salt);
                hmac.ComputeHash(Encoding.UTF8.GetBytes(key));

                return Convert.ToBase64String(hmac.Hash);
            }
        }
    }
}
