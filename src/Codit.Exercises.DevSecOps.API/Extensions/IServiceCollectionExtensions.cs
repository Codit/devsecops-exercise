using System;
using System.IO;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds Open API documentation generation
        /// </summary>
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            var openApiInformation = new OpenApiInfo
            {
                Title = "Codit - Warehouse API",
                Version = "v1"
            };

            services.AddSwaggerGen(swaggerGenerationOptions =>
            {
                swaggerGenerationOptions.SwaggerDoc("v1", openApiInformation);
                swaggerGenerationOptions.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Codit.Exercises.DevSecOps.Open-Api.xml"));
                swaggerGenerationOptions.OperationFilter<AddHeaderOperationFilter>("X-Transaction-Id", "Transaction ID is used to correlate multiple operation calls. A new transaction ID will be generated if not specified.", false);
                swaggerGenerationOptions.OperationFilter<AddResponseHeadersFilter>();
            });

            return services;
        }
    }
}