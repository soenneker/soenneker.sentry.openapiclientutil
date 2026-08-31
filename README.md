[![](https://img.shields.io/nuget/v/soenneker.sentry.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sentry.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.sentry.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.sentry.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.sentry.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.sentry.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.sentry.openapiclientutil/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.sentry.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Sentry.OpenApiClientUtil

Provides a lazily initialized Sentry client for organizations, projects, teams, issues, events, releases, alerts, integrations, and account resources.

## Installation

```bash
dotnet add package Soenneker.Sentry.OpenApiClientUtil
```

## Configuration

```json
{
  "Sentry": {
    "ApiKey": "your-sentry-auth-token"
  }
}
```

For self-hosted Sentry, set `Sentry:ClientBaseUrl` to the installation origin without appending `/api/0`.

## Usage

```csharp
using Soenneker.Sentry.OpenApiClientUtil.Abstract;
using Soenneker.Sentry.OpenApiClientUtil.Registrars;

services.AddSentryOpenApiClientUtilAsSingleton();

public sealed class SentryOrganizationReader
{
    private readonly ISentryOpenApiClientUtil _sentry;

    public SentryOrganizationReader(ISentryOpenApiClientUtil sentry)
    {
        _sentry = sentry;
    }

    public async Task GetOrganizations(CancellationToken cancellationToken)
    {
        var client = await _sentry.Get(cancellationToken);
        var organizations = await client.Api.Zero.Organizations.GetAsync(
            cancellationToken: cancellationToken);
    }
}
```

Use `AddSentryOpenApiClientUtilAsScoped()` when each scope should have its own lazily initialized API client. Both registrations reuse the singleton authenticated HTTP client provider.
