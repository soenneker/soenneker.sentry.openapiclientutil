using Soenneker.Sentry.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Sentry.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class SentryOpenApiClientUtilTests : HostedUnitTest
{
    private readonly ISentryOpenApiClientUtil _openapiclientutil;

    public SentryOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<ISentryOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
