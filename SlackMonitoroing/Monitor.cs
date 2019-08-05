using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SlackMonitoroing.Models;

namespace SlackMonitoroing
{
    public class Monitor
    {
        public string DbMonitor(DbMonitorModel monitor)
        {
            var averageQueryTime = Helpers.CheckQueryAverageTime(monitor.TypeOfQuery);
            var warningLevel = Helpers.DegreeOfError(monitor.Timer, averageQueryTime);

            if(warningLevel == TypeOfError.INFORMATIONAL || averageQueryTime == 0)
                Helpers.WriteToLog(monitor.TypeOfQuery, monitor.Timer);

            if (averageQueryTime == 0)
                monitor.TypeOfError = TypeOfError.INSUFFICIENTMODELDATA;

            PostToSlackAsync(new DbMonitorModel
            {
                TypeOfError = warningLevel,
                Timer = monitor.Timer,
                TypeOfQuery = monitor.TypeOfQuery,
                FunctionName = monitor.FunctionName,
                SlackWebhookUrl = monitor.SlackWebhookUrl
            });

            return "";
        }

        public static void PostToSlackAsync(DbMonitorModel model)
        {
            string request = ($"Function Name: {model.FunctionName}, Type of Error: {model.TypeOfError}, " +
                $" Type of Query: {model.TypeOfQuery},  Timer: {model.Timer}");
            string requestJson = $"{{'text': '{request}'}}";

            using (var client = new HttpClient())
            {
                var result = client.PostAsync(model.SlackWebhookUrl, 
                    new StringContent(requestJson, Encoding.UTF8, "application/json")).Result;
                if(result.IsSuccessStatusCode)
                {
                    var responseContent = result.Content;
                    string resultContent = responseContent.ReadAsStringAsync().Result;
                    Console.WriteLine(resultContent);
                }
            }
        }
    }

    
}
