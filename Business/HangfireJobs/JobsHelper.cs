using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.HangfireJobs
{
    public class JobsHelper
    {
        public static void RecurringJobs(string name, Expression<Action> method, string time)
        {
            RecurringJob.AddOrUpdate(name, method, time, TimeZoneInfo.Local);  
        }

        public static void Enqueue(Expression<Action> method)
        {
            BackgroundJob.Enqueue(method);
        }
    }
}
