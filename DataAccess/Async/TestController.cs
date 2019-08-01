using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Async
{
    class TestController
    {
        public async Task<int> TestAction()
        {
            var dbTask = LongTermOperations.LoadDbData();
            var wsTask = LongTermOperations.ConsumeWebService();
            var mlTask = LongTermOperations.SendNotificationMail();

            return await dbTask + await wsTask + await mlTask;
        }
    }
}
