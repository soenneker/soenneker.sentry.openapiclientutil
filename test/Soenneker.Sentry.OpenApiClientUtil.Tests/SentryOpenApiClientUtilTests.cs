using Soenneker.Sentry.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Sentry.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class SentryOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly ISentryOpenApiClientUtil _openapiclientutil;

    public SentryOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<ISentryOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
