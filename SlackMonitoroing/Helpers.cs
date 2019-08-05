using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlackMonitoroing.Models;

namespace SlackMonitoroing
{
    public static class Helpers
    {
        public static int CheckQueryAverageTime(TypeOfQuery query)
        {
            int averageQueryTime = 0;
            var lowerQuery = query.ToString().ToLower();
            try
            {
                var file = File.ReadAllLines(Constants.LOG_PATH + lowerQuery + ".txt");

                if (file.Length < 30)
                    return 0;

                switch (query)
                {
                    case TypeOfQuery.INSERT:
                        averageQueryTime = StringArrayToAverage(file);
                        break;

                    case TypeOfQuery.DELETE:
                        averageQueryTime = StringArrayToAverage(file);
                        break;

                    case TypeOfQuery.UPDATE:
                        averageQueryTime = StringArrayToAverage(file);
                        break;

                    case TypeOfQuery.SELECT:
                        averageQueryTime = StringArrayToAverage(file);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            return averageQueryTime;
        }

        public static TypeOfError DegreeOfError(int queryExecution, int averageQueryTime)
        {
            var info = (1.10 * averageQueryTime);
            var warning = (1.25 * averageQueryTime);
            var error = (1.50 * averageQueryTime);

            if (queryExecution <= info)
            {
                return TypeOfError.INFORMATIONAL;
            }
            if (queryExecution <= warning && queryExecution > info)
            {
                return TypeOfError.WARNING;
            }
            if (queryExecution <= error || queryExecution > warning)
            {
                return TypeOfError.ERROR;
            }

            return TypeOfError.NONE;
        }

        public static void WriteToLog(TypeOfQuery query, int timer)
        {
            try
            {
                var lowerQuery = query.ToString().ToLower();
                var file = Constants.LOG_PATH + lowerQuery + ".txt";
                var timerArray = Environment.NewLine + timer.ToString();

                switch (query)
                {
                    case TypeOfQuery.INSERT:
                        File.AppendAllText(file, timerArray);
                        break;

                    case TypeOfQuery.DELETE:
                        File.AppendAllText(file, timerArray);
                        break;

                    case TypeOfQuery.UPDATE:
                        File.AppendAllText(file, timerArray);
                        break;

                    case TypeOfQuery.SELECT:
                        File.AppendAllText(file, timerArray);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static int StringArrayToAverage(string[] lines)
        {
            int total = 0;
            foreach (var item in lines)
            {
                var time = int.Parse(item);

                total += time;
            }
            total = total / lines.Length;
            return total;
        }
    }
}
