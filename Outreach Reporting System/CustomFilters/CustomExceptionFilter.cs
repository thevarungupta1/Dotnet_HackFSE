using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Outreach_Reporting_System.CustomFilters
{
    public class CustomExceptionFilter: IExceptionFilter
    {
        private readonly ILogger _logger;
        ///<summary>
        ///CustomExceptionFilter constructor
        /// </summary>
        /// <param name="loggerFactory"></param>
        public CustomExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomExceptionFilter>();
        }

        ///<summary>
        ///OnException will get execute if any exception occurs
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            String message = String.Empty;

            var exceptionType = context.Exception.GetType();

            if(exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access";
                status = HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred";
                status = HttpStatusCode.InternalServerError;
            }
            else 
            {
                message = context.Exception.Message;
                status = HttpStatusCode.InternalServerError;
            }

            _logger.LogError((int)status, context.Exception,
                $"error while processing request: " +
                $"{context.HttpContext.Request.Path}");
            _logger.LogInformation("EndPoint URL: " +
                context.HttpContext.Request.Host +
                context.HttpContext.Request.Path);
            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            var err = JsonConvert.SerializeObject(new
            {
                response.StatusCode,
                message
            });
            _logger.LogError("Error-Details" + err);
        }

    }
}
