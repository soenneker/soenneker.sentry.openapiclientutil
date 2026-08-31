using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Sentry.HttpClients.Registrars;
using Soenneker.Sentry.OpenApiClientUtil.Abstract;

namespace Soenneker.Sentry.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the lazily initialized Sentry API client.
/// </summary>
public static class SentryOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds the Sentry API client utility as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddSentryOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddSentryOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ISentryOpenApiClientUtil, SentryOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds the Sentry API client utility as a scoped service backed by the singleton HTTP client provider. <para/>
    /// </summary>
    public static IServiceCollection AddSentryOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddSentryOpenApiHttpClientAsSingleton()
                .TryAddScoped<ISentryOpenApiClientUtil, SentryOpenApiClientUtil>();

        return services;
    }
}
