using System.Text.RegularExpressions;

namespace OpenGrokWebApi;

// https://opengrok.docs.apiary.io/#reference/0/web-app-version/get-web-app-version-as-string?console=1

public partial class OpenGrok : JsonService
{
    public OpenGrok(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }

    public OpenGrok(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    /// <summary>
    /// Configures the provided <see cref="HttpClient"/> instance with specific default headers required for API requests.
    /// This includes setting the User-Agent, Accept, and API version headers.
    /// </summary>
    /// <param name="client">The <see cref="HttpClient"/> to configure for GitHub API usage.</param>
    /// <param name="appName">The name of the application, used as the User-Agent header value.</param>
    protected override void InitializeClient(HttpClient client, string appName)
    {
        client.DefaultRequestHeaders.Add("User-Agent", appName);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    //protected override string? AuthenticationTestUrl => "system/ping";


    // https://shm-opengrok.elektrobit.com/source/api/v1/suggest/config

    // curl -H "Authorization: Basic YnM6VmlzdWFsRW50ZTYuMVNwMg==" https://shm-opengrok.elektrobit.com/source/


    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var html = await GetStringAsync("", cancellationToken);

        if (string.IsNullOrWhiteSpace(html)) return null;

        
        var match = VersionRegex().Match(html);
        string s = match.Success ? match.Groups[1].Value : "0.0.0";
        return s;
    }

    [GeneratedRegex(@"<meta\s+name=[""']generator[""'][^>]*content=[""']\{?OpenGrok\s+([0-9.]+)", RegexOptions.IgnoreCase, "de-DE")]
    private static partial Regex VersionRegex();
}
