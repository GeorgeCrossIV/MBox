using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MBox.Models
{
    public class ApacheLogRootobject
    {
        public ApacheLogData data { get; set; }
    }

    public class ApacheLogData
    {
        public ApacheLogs apacheLogs { get; set; }
    }

    public class ApacheLogs
    {
        public ApacheLog[] values { get; set; }
    }

    public class ApacheLog
    {
        public string logId { get; set; }
        public string ip { get; set; }
        public string accessUrl { get; set; }
    }
}
