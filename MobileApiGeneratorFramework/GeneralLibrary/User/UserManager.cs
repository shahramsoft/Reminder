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
        public  string EncryptPassword(string Password, string UserName, int key = 12)
        {
            string result;
            try
            {
                string[] array = new string[Password.Length];
                UserName = UserName.ToLower();
                string text = "";
                Encoding @default = Encoding.Default;
                byte[] array2 = new byte[UserName.Length];
                byte[] array3 = new byte[Password.Length];
                byte[] bytes = @default.GetBytes(UserName);
                byte[] bytes2 = @default.GetBytes(Password);
                int[] array4 = new int[bytes2.Length];
                for (int i = 0; i < bytes2.Length; i++)
                {
                    array4[i] = (int)bytes2[i];
                }
                short num = 0;
                while ((int)num < UserName.Length)
                {
                    byte b = Convert.ToByte((int)(bytes[(int)num] + Convert.ToByte(key)));
                    array2[(int)num] = b;
                    char[] array5 = new char[@default.GetCharCount(array2, 0, array2.Length)];
                    @default.GetChars(array2, 0, array2.Length, array5, 0);
                    text = new string(array5);
                    num += 1;
                }
                byte[] bytes3 = @default.GetBytes(text);
                short num2 = 0;
                while ((int)num2 < Password.Length)
                {
                    for (int j = 0; j < text.Length; j++)
                    {
                        array4[(int)num2] = (array4[(int)num2] ^ (int)bytes3[j]);
                        byte b2 = Convert.ToByte(array4[(int)num2]);
                        array3[(int)num2] = b2;
                        char[] array6 = new char[@default.GetCharCount(array3, 0, array3.Length)];
                        @default.GetChars(array3, 0, array3.Length, array6, 0);
                        array[(int)num2] = new string(array6);
                    }
                    num2 += 1;
                }
                result = array[Password.Length - 1];
            }
            catch
            {
                result = "";
            }
            return result;
        }
        public ResultViewModel Login (string userName,string password)
        {
            try
            {
                var isvalidData = false;
                var name = "";
                var family = "";
                password = EncryptPassword(password, userName);
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
                    password = EncryptPassword(password, userName);
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
