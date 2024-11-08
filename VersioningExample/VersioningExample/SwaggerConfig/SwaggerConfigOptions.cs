using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VersioningExample.SwaggerConfig
{
    public class SwaggerConfigOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;

        public SwaggerConfigOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var versionDescription in _apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(versionDescription.GroupName, new OpenApiInfo
                {
                    Title = "Example Versioning API",
                    Version = versionDescription.ApiVersion.ToString()
                });
            }
        }
    }
}
