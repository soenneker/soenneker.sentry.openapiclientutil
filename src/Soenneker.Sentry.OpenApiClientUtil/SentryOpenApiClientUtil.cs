using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Sentry.HttpClients.Abstract;
using Soenneker.Sentry.OpenApiClientUtil.Abstract;
using Soenneker.Sentry.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Sentry.OpenApiClientUtil;

///<inheritdoc cref="ISentryOpenApiClientUtil"/>
public sealed class SentryOpenApiClientUtil : ISentryOpenApiClientUtil
{
    private readonly AsyncSingleton<SentryOpenApiClient> _client;

    public SentryOpenApiClientUtil(ISentryOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<SentryOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Sentry:ApiKey");
            string authHeaderValueTemplate = configuration["Sentry:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

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
