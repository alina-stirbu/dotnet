using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Helpers
{
    public interface ICustomLogger
    {
        void Log(string message);
    }
    public class CustomLogger:ICustomLogger
    {
        private readonly ILogger<CustomLogger> logger;

        public CustomLogger(ILogger<CustomLogger> logger)
        {
            this.logger = logger;
        }

        public void Log(string message)
        {
            this.logger.LogInformation(message);
        }
    }
}
