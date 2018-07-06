using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderLibrary.ViewModel
{
    public class DevicePeriodScheduleViewModel
    {
        public string Id { get; set; }
        public string DeviceId { get; set; }
        public string UserId { get; set; }
        public string FirstDateServise { get; set; }
        public string ServiseTypeName { get; set; }
        public string PeriodType { get; set; }
        public string Period { get; set; }

    }
}
