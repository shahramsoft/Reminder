using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralLibrary
{
   public class ConnectionManager
    {
        public string ConnectionString { get; set; }
        public ConnectionManager()
        {
            ConnectionString = "Password = Abc1234; Persist Security Info = True; User ID = shahramf_Reminder; Initial Catalog = shahramf_Reminder; Data Source =alfred.r1host.com";
            //ConnectionString = "Password = Abc1234; Persist Security Info = True; User ID = sa; Initial Catalog = MobileApiGeneratorFramework; Data Source =.";
        }
    }
}
