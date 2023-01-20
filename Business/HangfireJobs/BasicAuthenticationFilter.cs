using HangfireBasicAuthenticationFilter;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.HangfireJobs
{
    public class BasicAuthenticationFilter
    {
        private static HangfireSettings _hangfireSettings;
        public static HangfireCustomBasicAuthenticationFilter Authorize(IConfiguration configuration)
        {
            _hangfireSettings = configuration.GetSection("HangfireSettings").Get<HangfireSettings>();

            return new HangfireCustomBasicAuthenticationFilter
            {
                User = _hangfireSettings.UserName,
                Pass = _hangfireSettings.Password
            };
        }
    }
}
