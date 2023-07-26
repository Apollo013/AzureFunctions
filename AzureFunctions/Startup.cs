using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs;
using System;
using Microsoft.Extensions.DependencyInjection;
using AzureFunctions.Data;
using Microsoft.EntityFrameworkCore;
using AzureFunctions;

[assembly: WebJobsStartup(typeof(Startup))]
namespace AzureFunctions
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("AzureSqlDatabase");

            builder.Services.AddDbContext<DbDataContext>(
                options => options.UseSqlServer(connectionString));

            builder.Services.BuildServiceProvider();

        }
    }
}