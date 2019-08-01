using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataAccess.Async
{
    public static class LongTermOperations
    {
        public static async Task<int> LoadDbData()
        {
            Random r = new Random();
            int operationTime = r.Next(3000, 5000);
           await Task.Delay(operationTime);

            return operationTime;
        }

        public static async Task<int> ConsumeWebService()
        {
            Random r = new Random();
            int operationTime = r.Next(3000, 5000);
            await Task.Delay(operationTime);

            return operationTime;
        }

        public static async Task<int> SendNotificationMail()
        {
            Random r = new Random();
            int operationTime = r.Next(3000, 5000);
            await Task.Delay(operationTime);

            return operationTime;
        }
}
}
