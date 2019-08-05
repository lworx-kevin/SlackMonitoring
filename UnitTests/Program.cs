using System;
using SlackMonitoroing;
using SlackMonitoroing.Models;

namespace UnitTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DbMonitorModel
            {
                FunctionName = "Select Users",
                SlackWebhookUrl = "https://hooks.slack.com/services/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",
                TypeOfQuery = TypeOfQuery.SELECT,
                Timer = 1030
            };
            var monitor = new Monitor().DbMonitor(db);
            Console.WriteLine("Hello World!");
        }
    }
}
