namespace LDP_APIs.Helpers.Helpers
{
    using LDP_APIs.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System;
    using System.Net;
    using System.Text.Json;
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new baseResponse()
            {
                 IsSuccess = false
            };
            switch (exception)
            {
                case ApplicationException ex:
                    if (ex.Message.Contains("Invalid Token"))
                    {
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        errorResponse.Message = ex.Message;
                        break;
                    }
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    errorResponse.Message = "Token invalid / expired";
                    errorResponse.errors = new List<string>() { ex.Message };
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorResponse.Message = "Please check logs ... some thing went wrong ";
                    errorResponse.errors = new List<string>() { GetExceptionDetails(exception) };
                    break;
            }
            _logger.LogError(exception.Message);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }

        private string  GetExceptionDetails(Exception exception)
        {
            string exceptionDtls = string.Empty;
            var stackTrace = new StackTrace(exception, true);
            var frame = stackTrace.GetFrame(0);
            var filename = frame.GetFileName();
            var lineNumber = frame.GetFileLineNumber();
            var className = frame.GetMethod().DeclaringType.FullName;
            string innerException = string.Empty;
            if (exception.InnerException != null) 
            {
                innerException = exception.InnerException.Message;
            }
            // Log the details
            exceptionDtls = $"Exception message : {exception.Message} , Inner exception : {innerException}, Exception occurred in {className} at {filename}:{lineNumber} , Exception : {exception}";


            return exceptionDtls;
        }
    }
}
