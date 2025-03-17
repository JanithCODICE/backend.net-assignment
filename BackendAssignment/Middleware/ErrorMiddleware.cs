using BackendASsignment.Model.Response;
using static BackendAssignment.Core.Exceptions.UserDefinedException;
using System.Net;
using System.Text.Json;

namespace BackendAssignment.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleware> _logger;

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                switch (exception)
                {
                    case UDInvalidOperationException:
                    case UDNotFoundException:
                    case UDUnauthorizedAccessException:
                    case UDArgumentException:
                    case UDValiationException:
                        response.StatusCode = (int)HttpStatusCode.Accepted;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _logger.LogError("BackendAssignment.API.ErrorHandlingMiddleware | Exception: {exception}", exception.ToString());
                Console.WriteLine($"Error: {exception.Message} \n {exception?.InnerException?.StackTrace}");
                const string defaultErrorMessage = "SERVERSIDE_ERROR_OCCURED";
                BaseResponse<string> exceptionResponse = new BaseResponse<string> { Message = response.StatusCode == (int)HttpStatusCode.InternalServerError ? defaultErrorMessage : exception?.Message };
                JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                string result = JsonSerializer.Serialize(exceptionResponse, serializeOptions);
                await response.WriteAsync(result);
            }
        }
    }
}
