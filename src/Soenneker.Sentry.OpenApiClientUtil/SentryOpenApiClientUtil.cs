using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Sentry.HttpClients.Abstract;
using Soenneker.Sentry.OpenApiClientUtil.Abstract;
using Soenneker.Sentry.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Sentry.OpenApiClientUtil;

public sealed class SentryOpenApiClientUtil : ISentryOpenApiClientUtil
{
    private readonly AsyncSingleton<SentryOpenApiClient> _client;

    public SentryOpenApiClientUtil(ISentryOpenApiHttpClient httpClientUtil, IConfiguration _)
    {
        _client = new AsyncSingleton<SentryOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient)
            {
                BaseUrl = httpClient.BaseAddress!.ToString().TrimEnd('/')
            };

            return new SentryOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<SentryOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
