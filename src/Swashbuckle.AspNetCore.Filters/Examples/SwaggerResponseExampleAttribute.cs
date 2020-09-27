using System;

namespace Swashbuckle.AspNetCore.Filters
{
    /// <inheritdoc />
    /// <summary>
    /// This is used for generating Swagger documentation. Should be used in conjuction with SwaggerResponse - will add examples to SwaggerResponse.
    /// See https://github.com/mattfrear/Swashbuckle.AspNetCore.Examples
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerResponseExampleAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        /// Add custom example data to your SwaggerResponse
        /// </summary>
        /// <param name="statusCode">The HTTP status code, e.g. 200</param>
        /// <param name="examplesProviderType">A type that inherits from IExamplesProvider</param>
        /// <param name="contractResolver">An optional json contract Resolver if you want to override the one you use</param>
        /// <param name="jsonConverter">An optional jsonConverter to use, e.g. typeof(StringEnumConverter) will render strings as enums</param>
        public SwaggerResponseExampleAttribute(int statusCode, Type examplesProviderType)
        {
            StatusCode = statusCode;
            ExamplesProviderType = examplesProviderType;
        }

        public Type ExamplesProviderType { get; }

        public int StatusCode { get; }
    }
}
