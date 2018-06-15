using GeneralLibrary.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderLibrary.Logic
{
   public class DeviceManager:GeneralLibrary.ConnectionManager
    {
        public DeviceManager():base()
        {

        }

     

        public ResultViewModel CreateDevice (string deviceName, int userId)
        {
            try
            {
                var commandnitgen = @"insert into Device (UserId,DeviceName) values ("+ userId + ",'"+ deviceName + "')";
                var conn = new SqlConnection(ConnectionString);

                using (var cmd = new SqlCommand(commandnitgen, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    var reader = cmd.ExecuteNonQuery();

                    conn.Close();
                    conn.Dispose();
                    //reader.Close();
                }
                return new ResultViewModel
                {
                    Validate = true,
                    //Message = JsonConvert.SerializeObject(userObj)
                    ValidateMessage = "ثبت با موفقیت به تمام رسید"

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
        public ResultViewModel GetUserDevices(string userid)
        {
            try
            {
                var listDevices = new List<ViewModel.DeviceViewModel>();
                var isvalidData = false;
                   var commandnitgen = @"select * from Device where UserId='" + userid + "'";
                var conn = new SqlConnection(ConnectionString);

                using (var cmd = new SqlCommand(commandnitgen, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        isvalidData = true;
                       var name = reader["DeviceName"].ToString();
                        var id = reader["Id"].ToString();
                        listDevices.Add(new ViewModel.DeviceViewModel
                        {
                            Id=id,
                            Name=name
                        });

                    }
                    conn.Close();
                    conn.Dispose();
                    reader.Close();
                }
                if (isvalidData)
                {
                    
                    return new ResultViewModel
                    {
                        Validate = true,
                        Message = JsonConvert.SerializeObject(listDevices)

                    };
                }
                return new ResultViewModel
                {
                    Validate = false,
                    Message = "",
                    ValidateMessage = "هیچ اطلاعاتی جهت نمایش وجود ندارد"
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
