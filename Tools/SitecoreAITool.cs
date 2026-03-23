using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace McpSitecoreAiExample;

[McpServerToolType]
public static class SitecoreAITool
{

    /// <summary>
    /// This mcp server tool method retrieves the child details of a Sitecore item based on the provided path or item ID and language using the SitecoreAI Edge Service 
    /// and returns them as a JSON string. 
    /// </summary>
    /// <param name="path">The path of the Sitecore item.</param>
    /// <param name="language">The language of the Sitecore item.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A JSON string containing the child details of the Sitecore item.</returns>
    [McpServerTool, Description("Retrieves child item details from Sitecore by specifying an item path or ID and language.")]
    public static async Task<string> GetChildDetails(SitecoreAIService sitecoreAIService,
            [Description("The item path (e.g., /sitecore/content/site-name/home) or item GUID (e.g., '{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}')")] string path,
            [Description("Language code for the item (e.g., 'en' for English, 'de' for German). Default is 'en'.")] string language = "en",
            CancellationToken cancellationToken = default)
    {
        var childDetails = await sitecoreAIService.GetChildDetails(path, language);
        return JsonSerializer.Serialize(childDetails);
    }
}