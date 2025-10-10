using System.Xml.XPath;

namespace OpenGrokWebApi.Service;


// http://shm-OpenGrok/api/health

internal class OpenGrokService(Uri host, IAuthenticator? authenticator, string appName)
    : JsonService(host, authenticator, appName, SourceGenerationContext.Default)
{
    
    protected override string? AuthenticationTestUrl => "source";

    //protected override async Task ErrorCheckAsync(HttpResponseMessage response, string memberName, CancellationToken cancellationToken)
    //{
    //    await base.ErrorCheckAsync(response, memberName, cancellationToken);

    //    string str = await response.Content.ReadAsStringAsync(cancellationToken);
    //    if (str.StartsWith("{\"status\":\"error\""))
    //    {
    //        //var res = await ReadFromJsonAsync<ResultModel>(response, cancellationToken);

    //        JsonTypeInfo<ResultModel> jsonTypeInfo = (JsonTypeInfo<ResultModel>)context.GetTypeInfo(typeof(ResultModel))!;
    //        var res = await response.Content.ReadFromJsonAsync<ResultModel>(jsonTypeInfo, cancellationToken);

    //        throw new WebServiceException(res!.Messages);
    //    }
    //}


    //https://shm-opengrok.elektrobit.com/source/api/v1/


    public override async Task<Version> GetVersionAsync(CancellationToken cancellationToken)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetStringAsync("source", cancellationToken);
        var doc = XDocument.Parse(res!);
        var root = doc.Root!;
        var elm = root.XPathSelectElement("./meta[@name='generator']");
        var ver = elm?.Attribute("content")?.Value ?? "0,0,0,0";
        return new Version(ver);
    }

    public override async Task<string> GetVersionStringAsync(CancellationToken cancellationToken)
    {
        WebServiceException.ThrowIfNotConnected(client);

        var res = await GetStringAsync("source", cancellationToken);
        var doc = XDocument.Parse(res!);
        var root = doc.Root!;
        var elm = root.XPathSelectElement("./meta[@name='generator']");
        return elm?.Attribute("content")?.Value ?? "";
    }
    //    <meta name = "generator" content="{OpenGrok 1.12.25 (a84b4100bdb836b4c4ae75d0dfbe9ef1f6742440)">
}
