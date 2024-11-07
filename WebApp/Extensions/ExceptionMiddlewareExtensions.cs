using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using System.Net;

namespace WebApp.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    //hata mesajını özelleştiriyor.
                    var featureContext = context.Features.Get<IExceptionHandlerFeature>();
                    //null ise hata yoktur o yüzden not null durumu kontrol edicek.

                    if (featureContext is not null)
                    {
                        context.Response.StatusCode = featureContext.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            
                            _ => StatusCodes.Status500InternalServerError,
                        };
                        logger.LogError($"Something went wrong: {featureContext.Error}");
                        //log ataması yapıyor.
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = featureContext.Error.Message
                        }.ToString());
                    }
                });
            }
            );
        }
    }
}
