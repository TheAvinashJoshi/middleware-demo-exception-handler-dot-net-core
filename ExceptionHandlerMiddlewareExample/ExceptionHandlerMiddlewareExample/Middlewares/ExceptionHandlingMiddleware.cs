using System.Net;

namespace ExceptionHandlerMiddlewareExamples
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private const string GENERIC_ERROR_MESSAGE = "An unexpected error occurred. Please try again later.";
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Proceed to next middleware or endpoint
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    IsSuccess = false,
                    Message = GENERIC_ERROR_MESSAGE,
                    ErrorDetails = ex
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
