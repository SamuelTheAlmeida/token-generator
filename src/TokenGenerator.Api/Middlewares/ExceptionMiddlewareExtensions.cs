using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using TokenGenerator.Domain.Command.Result;
using TokenGenerator.Domain.Enums;

namespace TokenGenerator.Api.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, bool internalServerErrorWithException)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                    var exception = contextFeature.Error.InnerException ?? contextFeature.Error;

                    var operationResult = new ApplicationResult<Exception>(success: false, message: DefaultResults.InternalError, data: (internalServerErrorWithException ? exception : null));

                    string json = JsonConvert.SerializeObject(operationResult,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() },
                            Formatting = Formatting.Indented,
                            NullValueHandling = NullValueHandling.Ignore
                        });

                    await context.Response.WriteAsync(json);
                });
            });
        }
    }
}
