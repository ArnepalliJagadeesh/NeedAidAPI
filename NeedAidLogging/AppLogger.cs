using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeedAidLogging
{
    public class AppLogger
    {
        public static NeedAidLogger Log { get; set; }

        static AppLogger()
        {
            Log = new NeedAidLogger();
            Log.ApplicationName = "Need Aid";
        }
    }
}
