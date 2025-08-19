namespace OpenGrokWebApi;

// https://opengrok.docs.apiary.io/#reference/0/web-app-version/get-web-app-version-as-string?console=1

public class OpenGrok : JsonService
{
    public OpenGrok(string storeKey, string appName) : base(storeKey, appName, SourceGenerationContext.Default)
    { }

    public OpenGrok(Uri host, IAuthenticator? authenticator, string appName) : base(host, authenticator, appName, SourceGenerationContext.Default)
    { }

    //protected override string? AuthenticationTestUrl => "system/ping";


    public override async Task<string?> GetVersionStringAsync(CancellationToken cancellationToken = default)
    {
        WebServiceException.ThrowIfNotConnected(client);

        //var res0 = await GetStringAsync("system", cancellationToken);
        try
        {
            var res = await GetStringAsync("api/v1/system/ping", cancellationToken);
        }
        catch (Exception) { }

        try
        {
            var res = await GetStringAsync("api/v1/system/version", cancellationToken);
        }
        catch (Exception) { }

       
        return "0.0.0";
    }
}
