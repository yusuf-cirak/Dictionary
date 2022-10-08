using Dictionary.WebApi.Infrastructure.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;

namespace Dictionary.WebApi.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app, bool includeExceptionDetails = false, bool useDefaultHandlingResponse = true, Func<HttpContext, Exception, Task> handleException = null)
        {

            app.UseExceptionHandler(opt =>
            {
                opt.Run(context =>
                {
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                    if (!useDefaultHandlingResponse && handleException == null)
                        throw new ArgumentNullException($"{nameof(handleException)} cannot be null when {nameof(useDefaultHandlingResponse)} is false");


                    if (!useDefaultHandlingResponse && handleException != null)
                        return handleException(context, exceptionObject.Error);

                    return DefaultHandleException(context, exceptionObject.Error, includeExceptionDetails);
                });
            });
            

            return app;
        }

        private static async Task DefaultHandleException(HttpContext context, Exception exception, bool includeExceptionDetails)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Internal server error occured";

            ValidationResponseModel validationResponse;

            if (exception is UnauthorizedAccessException)
            {
                statusCode = HttpStatusCode.Unauthorized;

                validationResponse = new(exception.Message);
                await WriteResponse(context, statusCode, validationResponse);
                return;
            }



            statusCode = HttpStatusCode.BadRequest;
            validationResponse = new(exception.Message);
            await WriteResponse(context, statusCode, validationResponse);
            return;
        }

        private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, object responseObject)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(responseObject);
        }
    }
}
