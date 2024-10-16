using Entities.ErrorModel;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using System.Net;

namespace WebApp.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app,ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    //hata mesajını özelleştiriyor.
                    var featureContext = context.Features.Get<IExceptionHandlerFeature>();
                    //null ise hata yoktur o yüzden not null durumu kontrol edicek.

                    if(featureContext is not null)
                    {
                        logger.LogError($"Something went wrong: {featureContext.Error}");
                        //log ataması yapıyor.
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            }
            );
        }
    }
}
