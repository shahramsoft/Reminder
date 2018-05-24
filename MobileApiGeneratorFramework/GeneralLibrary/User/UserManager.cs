using GeneralLibrary.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLibrary.User
{
   public class UserManager : ConnectionManager
    {
        public UserManager():base()
        {

        }
        public ResultViewModel Login (string userName,string password)
        {
            try
            {
                var isvalidData = false;
                var name = "";
                var family = "";
                var commandnitgen = @"select * from [User] where Username='"+userName+"' and Password='"+password+"'";
                var conn = new SqlConnection(ConnectionString);

                using (var cmd = new SqlCommand(commandnitgen, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                         name = reader["Name"].ToString();
                         family = reader["Family"].ToString();
                        isvalidData = true;
                    }
                    conn.Close();
                    conn.Dispose();
                    reader.Close();
                }
                if (isvalidData)
                {
                    var userObj = new
                    {
                        Name = name,
                        Family = family
                    };
                    return new ResultViewModel
                    {
                        Validate = true,
                        Message = JsonConvert.SerializeObject(userObj)
                            
                    };
                }
                return new ResultViewModel
                {
                    Validate = false,
                    Message = "",
                    ValidateMessage = "نام کاربری یا رمز عبور اشتباه است"
                };
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

        public ResultViewModel RegisterUser(string userName, string password, string name,string family)
        {
            try
            {
                var isvalidData = false;
                //var name = "";
                //var family = "";
                var commandnitgen = @"select * from [User] where Username='" + userName + "'";
                var conn = new SqlConnection(ConnectionString);

                using (var cmd = new SqlCommand(commandnitgen, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        //name = reader["Name"].ToString();
                        //family = reader["Family"].ToString();
                        isvalidData = true;
                    }
                    conn.Close();
                    conn.Dispose();
                    reader.Close();
                }
                if (!isvalidData)
                {
                     commandnitgen = @"insert into [User] (Username,[Password],Name,Family) values ('"+userName+"','"+password+"','"+name+"','"+family+"')";
                     conn = new SqlConnection(ConnectionString);

                    using (var cmd = new SqlCommand(commandnitgen, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        conn.Open();
                        var resultExecute = cmd.ExecuteNonQuery();
                       
                        conn.Close();
                        conn.Dispose();
                        
                    }


                    return new ResultViewModel
                    {
                        Validate = true,
                        //Message = JsonConvert.SerializeObject(userObj)
                        ValidateMessage="ثبت با موفقیت به تمام رسید لطفا با نام کاربری انتخابی خود وارد شوید"

                    };
                }
                return new ResultViewModel
                {
                    Validate = false,
                    Message = "",
                    ValidateMessage = "نام کاربری تکراری است"
                };
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

    }
}
