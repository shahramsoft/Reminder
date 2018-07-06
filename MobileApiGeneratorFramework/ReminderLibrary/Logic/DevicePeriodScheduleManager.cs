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
    public class DevicePeriodSheduleManager : GeneralLibrary.ConnectionManager
    {
        public DevicePeriodSheduleManager() : base()
        {

        }
        public ResultViewModel CreateDevicePeriodSchedule(string deviceId, string userId, string firstDateServise, string serviceTypeName, string periodType, string period)
        {
            try
            {
                var commandnitgen = @"insert into DevicePeriodSchedule (DeviceId,UserId,FirstDateService,ServiceTypeName,PeriodType,Period) values ('" + deviceId + "','" + userId + "','" + firstDateServise + "','" + serviceTypeName + "','" + periodType + "','" + period + "')";
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
        public ResultViewModel GetDevicePeriodScheduls(string Id)
        {
            try
            {
                var listDevicesPeriodShedule = new List<ViewModel.DevicePeriodScheduleViewModel>();
                var isvalidData = false;
                var commandnitgen = @"select * from DevicePeriodSchedule where Id='" + Id + "'";
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
                        var deviceId = reader["DeviceId"].ToString();
                        var userId = reader["UserId"].ToString();
                        var firstDateServise = reader["FirstDateService"].ToString();
                        var serviseTypeName = reader["ServiceTypeName"].ToString();
                        var periodType = reader["PeriodType"].ToString();
                        var period = reader["Period"].ToString();
                        listDevicesPeriodShedule.Add(new ViewModel.DevicePeriodScheduleViewModel
                        {
                            Id = id,
                            DeviceId = deviceId,
                            UserId = userId,
                            FirstDateServise = firstDateServise,
                            ServiseTypeName = serviseTypeName,
                            PeriodType = periodType,
                            Period = period
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
                        Message = JsonConvert.SerializeObject(listDevicesPeriodShedule)

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
