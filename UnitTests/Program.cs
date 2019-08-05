using System;
using SlackMonitoroing;
using SlackMonitoroing.Models;

namespace UnitTests
{
    class Program
    {
        public static readonly string SlackURL = "https://hooks.slack.com/services/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";

        static void Main(string[] args)
        {
            var random = new Random().Next(1000, 8000);

            DbMonitorTest(random);
        }

        public static void DbMonitorTest(int timer)
        {
            var db = new DbMonitorModel
            {
                FunctionName = "Insert Users",
                SlackWebhookUrl = SlackURL,
                TypeOfQuery = TypeOfQuery.INSERT,
                Timer = timer
            };

            var monitor = new Monitor().DbMonitor(db);
        }
    }
}
