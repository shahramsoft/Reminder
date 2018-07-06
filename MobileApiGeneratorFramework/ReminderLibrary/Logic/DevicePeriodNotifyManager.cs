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
    public class DevicePeriodNotifyManager : GeneralLibrary.ConnectionManager
    {
        public DevicePeriodNotifyManager() : base()
        {
        }
        public ResultViewModel CreateDevicePeriodNotify(string devicePeriodId, string dateTimePeriod)
        {
            try
            {
                var commandnitgen = @"insert into DevicePeriodNotify (DevicePeriodId,DateTimePeriod) values ('" + devicePeriodId + "','" + dateTimePeriod + "')";
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
        public ResultViewModel GetDevicePeriodNotify(string Id)
        {
            try
            {
                var listDevicesPeriodNotify = new List<ViewModel.DevicePeriodNotifyViewModel>();
                var isvalidData = false;
                var commandnitgen = @"select * from DevicePeriodNotify where Id='" + Id + "'";
                var conn = new SqlConnection(ConnectionString);

                using (var cmd = new SqlCommand(commandnitgen, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    conn.Open();
                    var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        isvalidData = true;
                        var id = reader["Id"].ToString();
                        var devisePeriodId = reader["DevicePeriodId"].ToString();
                        var dateTimePeriod = reader["DateTimePeriod"].ToString();
                        listDevicesPeriodNotify.Add(new ViewModel.DevicePeriodNotifyViewModel
                        {
                            Id = id,
                            devicePeriodId = devisePeriodId,
                            dateTimePeriod = dateTimePeriod
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
                        Message = JsonConvert.SerializeObject(listDevicesPeriodNotify)

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

