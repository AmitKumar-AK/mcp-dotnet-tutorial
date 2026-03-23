using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace McpSitecoreAiExample;

public class SitecoreAIService
{
    HttpClient httpClient;
    public SitecoreAIService()
    {
        this.httpClient = new HttpClient();  
    }

    /// <summary>
    /// This method fetches Sitecore item details including its children by path or item id and language using an SaaS Edge GraphQL endpoint.
    /// </summary>
    /// <param name="path">The path of the Sitecore item.</param>
    /// <param name="language">The language of the Sitecore item.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A JSON string containing the item details and its children.</returns>
    public async Task<string> GetChildDetails(string path, string language, CancellationToken cancellationToken = default)
    {
        // Replace with your actual Sitecore AI Edge API endpoint and key
        string edgeGQLURL = "[SITECOREAI EDGE API END-POINT]"; // "https://edge.sitecorecloud.io/api/graphql/v1";
        string edgeGQLKey = "[SITECOREAI EDGE API KEY]";

        try
        {
            // Create HTTP POST request to get data from Sitecore AI Edge GraphQL API
            var request = new HttpRequestMessage(HttpMethod.Post, edgeGQLURL);

            // Add API key to request headers for authentication
            request.Headers.Add("sc_apikey", edgeGQLKey);

            // Construct Edge GraphQL query to fetch parent and child item details by path and language
            var query = $@"
                query Item {{
                    item(language: ""{language}"", path: ""{path}"") {{
                        id
                        name
                        path
                        displayName
                        hasChildren
                        children
                        {{
                            results
                            {{
                                id
                                name
                                path
                            }}
                        }}
                    }}
                }}";


            // Serialize the Child Item Edge GraphQL query and variables into JSON format for the request body
            var reqeustJSON = JsonSerializer.Serialize(new { query, variables = new { } });

            // Set the request content with the serialized JSON query
            var content = new StringContent(reqeustJSON, System.Text.Encoding.UTF8, "application/json");
            request.Content = content;

            // Send the HTTP request and get the response from Sitecore AI Edge GraphQL API
            var response = await httpClient.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();

            // Read and return the response content as a string
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // Handle any exceptions that occur during the API call and return an error message
            return $"Error: {ex.Message}";
        }
    }

}