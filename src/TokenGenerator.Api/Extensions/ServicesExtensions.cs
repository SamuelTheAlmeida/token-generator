using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;

namespace TokenGenerator.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureWebApi(this IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation()
                .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    opt.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                    opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true; });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "TokenGenerator.Api", 
                    Version = "v1",
                    Description = "Token Generation API for cashless registration",
                    Contact = new OpenApiContact
                    {
                        Name = "Samuel T. Almeida",
                        Email = "samuel.t.almeida@gmail.com",
                        Url = new Uri("http://github.com/samuelTheAlmeida/")
                    },
                });
            });
        }
    }
}
