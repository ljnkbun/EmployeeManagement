using Core.Exceptions;
using Core.Models.Response;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Core.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message!, Data = error?.InnerException?.Message! };

                if (!response.HasStarted)
                {
                    switch (error)
                    {
                        case ApiException:
                            // custom application error
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            break;
                        case ValidationException e:
                            // custom application error
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            List<Error> errors = new();
                            e.Errors.ForEach(x => errors.Add(new Error() { Message = x }));
                            responseModel.Errors = errors;
                            break;
                        case KeyNotFoundException:
                            // not found error
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;
                        default:
                            // unhandled error
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }
                }
                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
