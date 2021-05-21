namespace MediaStudio.Classes
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using MediaStudio.Classes.MyException;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.BadRequest; // 400 if unexpected

            if (exception is MyNotFoundException) code = HttpStatusCode.NotFound;
            else if (exception is MyUnauthorizedException) code = HttpStatusCode.Unauthorized;
            else if (exception is MyBadRequestException) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(exception.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
