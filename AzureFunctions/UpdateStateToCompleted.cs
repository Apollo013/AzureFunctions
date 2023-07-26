using System;
using System.Collections.Generic;
using System.Linq;
using AzureFunctions.Data;
using AzureFunctions.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureFunctions
{
    public class UpdateStateToCompleted
    {
        private readonly DbDataContext _db;

        public UpdateStateToCompleted(DbDataContext db)
        {
            _db = db;
        }

        [FunctionName("UpdateStateToCompleted")]
        public void Run([TimerTrigger("0 */1 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            IEnumerable<SalesRequest> salesRequests = _db.SalesRequests.Where(r => r.Status == "Submitted");

            foreach (SalesRequest request in salesRequests)
            {
                request.Status = "Completed";
            }

            _db.UpdateRange(salesRequests);
            _db.SaveChanges();
        }
    }
}
