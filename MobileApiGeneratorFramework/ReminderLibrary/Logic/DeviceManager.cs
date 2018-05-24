using GeneralLibrary.ViewModel;
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
        public ResultViewModel CreateDevice (string deviceName, int userId,DateTime firstTimeChecked,int periodOfChecks)
        {
            try
            {
                var commandnitgen = @"insert into Device (UserId,DeviceName,FirstTimeCheck,PeriodOfChecksPerMonths) values ("+ userId + ",'"+ deviceName + "','"+ firstTimeChecked + "',"+ periodOfChecks + ")";
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
    }
}
