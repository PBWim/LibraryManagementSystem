using System.Net;
using Library.Web.Models;
using Newtonsoft.Json;

namespace Library.Web.Configuration
{
    public class ExceptionMiddleware
	{
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Catches exceptions thrown during request processing.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpected error: {ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Formats and sends a generic error response to the client.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "text/html";

            var errorResponse = new ErrorViewModel
            {
                StatusCode = context.Response.StatusCode,
                Message = $"Internal Server Error : {ex.Message}"
            };

            await context.Response.WriteAsync("<html lang=\"en\"><body>\r\n");
            await context.Response.WriteAsync("<h1>Something went wrong!</h1>\r\n");
            await context.Response.WriteAsync("<p>We are sorry, but something went wrong. Please try again later.</p>\r\n");
            await context.Response.WriteAsync($"<p>{JsonConvert.SerializeObject(errorResponse)}</p>\r\n");
            await context.Response.WriteAsync("</body></html>\r\n");         
        }
    }
}