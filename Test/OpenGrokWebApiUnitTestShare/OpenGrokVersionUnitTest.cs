namespace OpenGrokWebApiUnitTest;

[TestClass]
public class OpenGrokVersionUnitTest : OpenGrokBaseUnitTest
{
    [TestMethod]
    public async Task TestMethodGetUserAsync()
    {
        using var OpenGrok = new OpenGrok(storeKey, appName);

        var version = await OpenGrok.GetVersionAsync();

        Assert.IsNotNull(version);
        Assert.AreEqual(new Version(11,6,1), version, nameof(version));
    }
}
