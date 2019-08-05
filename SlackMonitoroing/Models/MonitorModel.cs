using System;
using System.Collections.Generic;
using System.Text;

namespace SlackMonitoroing.Models
{
    public class DbMonitorModel : MonitorModel
    {
        public TypeOfQuery TypeOfQuery { get; set; }

        public string ToError { get; set; }
    }

    public class MonitorModel
    {
        public string FunctionName { get; set; }
        public TypeOfError TypeOfError { get; set; }
        public int Timer { get; set; }
        public string SlackWebhookUrl { get; set; }
    }

}
