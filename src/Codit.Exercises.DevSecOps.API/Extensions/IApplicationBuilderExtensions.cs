// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        ///     Adds Open API documentation UI
        /// </summary>
        public static IApplicationBuilder UseOpenApi(this IApplicationBuilder app)
        {
            app.UseSwagger(swaggerOptions =>
            {
                swaggerOptions.RouteTemplate = "api/{documentName}/docs.json";
            });
            app.UseSwaggerUI(swaggerUiOptions =>
            {
                swaggerUiOptions.SwaggerEndpoint("/api/v1/docs.json", "Codit.Exercises.DevSecOps");
                swaggerUiOptions.RoutePrefix = "api/docs";
                swaggerUiOptions.DocumentTitle = "Codit - Warehouse API";
            });

            return app;
        }
    }
}
