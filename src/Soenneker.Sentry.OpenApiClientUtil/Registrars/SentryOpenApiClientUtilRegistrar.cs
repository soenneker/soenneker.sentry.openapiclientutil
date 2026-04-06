using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Sentry.HttpClients.Registrars;
using Soenneker.Sentry.OpenApiClientUtil.Abstract;

namespace Soenneker.Sentry.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class SentryOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="SentryOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddSentryOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddSentryOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ISentryOpenApiClientUtil, SentryOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="SentryOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddSentryOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddSentryOpenApiHttpClientAsSingleton()
                .TryAddScoped<ISentryOpenApiClientUtil, SentryOpenApiClientUtil>();

        return services;
    }
}
