using System;
using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

namespace McpSitecoreAiExample;

[McpServerToolType]
public static class SellingTool
{
    [McpServerTool, Description("Get sellings.")]
    public static async Task<string> GetSellings(SellingService sellingService)
    {
        var sellings = await sellingService.GetAllSellingsAsync();
        return JsonSerializer.Serialize(sellings);
    } 
}