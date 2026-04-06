using Soenneker.Sentry.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Sentry.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ISentryOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<SentryOpenApiClient> Get(CancellationToken cancellationToken = default);
}
