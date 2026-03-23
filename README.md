# MCP C# SDK Tutorial with SitecoreAI Example

Welcome to this hands-on guide for creating powerful Model Context Protocol (MCP) tools using C# and .NET! If you're a developer looking to bridge AI capabilities with your applications, this tutorial will walk you through building custom MCP servers from scratch. We'll explore the official C# SDK and demonstrate everything with a practical SitecoreAI integration example.

## 🚀 What You'll Learn

- **Master MCP Fundamentals**: Understand how the Model Context Protocol enables seamless AI tool integration
- **Build C# MCP Servers**: Step-by-step guide using the official Microsoft SDK
- **Real-World SitecoreAI Example**: Create an MCP tool that interacts with Sitecore content and AI features
- **Testing & Deployment**: Get your tools running in VS Code and beyond

<!-- TABLE OF CONTENTS -->

<details>
  <summary> <span id="table-of-contents" style="font-size: 24px;font-weight: 600;"> Table of Contents</span> </summary>

- [MCP C# SDK Tutorial with SitecoreAI Example](#mcp-c-sdk-tutorial-with-sitecoreai-example)
  - [🚀 What You'll Learn](#-what-youll-learn)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Your First MCP Tool](#your-first-mcp-tool)
    - [Step 1: Build the Project](#step-1-build-the-project)
    - [Step 2: Define Your Tool](#step-2-define-your-tool)
    - [Step 3: Build the Solution](#step-3-build-the-solution)
    - [Step 4: Run and Test](#step-4-run-and-test)
  - [SitecoreAI Integration Example](#sitecoreai-integration-example)
    - [The SitecoreAI Tool](#the-sitecoreai-tool)
    - [Implementation](#implementation)
  - [Testing Your MCP Server](#testing-your-mcp-server)
    - [VS Code Integration](#vs-code-integration)
    - [Manual Testing](#manual-testing)
  - [Advanced Features](#advanced-features)
  - [Troubleshooting](#troubleshooting)
  - [Contributing](#contributing)
  - [Contributing Guide](#contributing-guide)
  - [License](#license)
  - [Acknowledgments](#acknowledgments)
  - [Additional Resources](#additional-resources)

</details>
<br/>

## Prerequisites

Before diving in, ensure you have:

- **.NET SDK** (version 8.0 or later) - [Download here](https://dotnet.microsoft.com/download)
- **Visual Studio Code** or **Visual Studio 2022** for development
- **Basic C# knowledge** - Familiarity with .NET and async programming helps
- **SitecoreAI (FKA XM Cloud) Platform** access

[⬆️ Go To Top](#table-of-contents)

## Installation

Let's get your development environment ready:

1. **Install the MCP C# SDK**:

   ```bash
   dotnet add package ModelContextProtocol --prerelease
   ```

2. **Clone this repository**:

   ```bash
   git clone https://github.com/AmitKumar-AK/mcp-dotnet-tutorial.git
   cd mcp-dotnet-tutorial
   ```

3. **Restore dependencies**:

   ```bash
   dotnet restore
   ```

The SDK provides three main packages:

- `ModelContextProtocol.Core` - Minimal dependencies for core functionality
- `ModelContextProtocol` - Full hosting and DI extensions
- `ModelContextProtocol.AspNetCore` - HTTP-based server capabilities

[⬆️ Go To Top](#table-of-contents)

## Your First MCP Tool

Let's start with a simple "Hello World" MCP tool to understand the basics.

### Step 1: Build the Project
Restore the packages and build the solution.
```bash
dotnet build
```

### Step 2: Define Your Tool

Create a `GreetingTool.cs` file:

```csharp
using System.ComponentModel;
using ModelContextProtocol.Server;

[McpServerToolType]
public static class GreetingTool
{
    [McpServerTool, Description("Hellow World message")]
    public static string GenerateGreeting(string message)
    {
        return $"Welcome to MCP tools: {message}"
    }
}
```

### Step 3: Build the Solution
Save the file and build the solution.
```bash
dotnet build
```


### Step 4: Run and Test

```bash
dotnet run
```

Your MCP server is now running and ready to accept requests!

[⬆️ Go To Top](#table-of-contents)

## SitecoreAI Integration Example

Now let's build something more powerful - an MCP tool that integrates with SitecoreAI for content analysis and optimization.

### The SitecoreAI Tool

This example demonstrates how to create an MCP tool that:

- Analyzes Sitecore content items
- Provides AI-powered content suggestions
- Integrates with Sitecore's headless services

### Implementation

```csharp
using System.ComponentModel;
using System.Text.Json;
using ModelContextProtocol.Server;

[McpServerToolType]
public static class SitecoreAiTool
{
   
    [McpServerTool, Description("Retrieves child item details from Sitecore by specifying an item path or ID and language.")]
    public static async Task<string> GetChildDetails(SitecoreAIService sitecoreAIService,
            [Description("The item path (e.g., /sitecore/content/site-name/home) or item GUID (e.g., '{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}')")] string path,
            [Description("Language code for the item (e.g., 'en' for English, 'de' for German). Default is 'en'.")] string language = "en",
            CancellationToken cancellationToken = default)
    {
        var childDetails = await sitecoreAIService.GetChildDetails(path, language);
        return JsonSerializer.Serialize(childDetails);
    }
    // Check rest of the implementation at the Code Repo
}
```


[⬆️ Go To Top](#table-of-contents)

## Testing Your MCP Server

### VS Code Integration

1. **Create `.vscode/mcp.json`**:
   ```json
    "mcp-sitecoreai": {
        "type": "stdio",
        "command": "dotnet",
        "args": [
            "run",
            "--project",
            "McpSitecoreAiExample.csproj",
        ]
    }
   ```

2. **Add the server in VS Code**:
   - Open Command Palette (Ctrl+Shift+P)
   - Type "MCP: Add Server"
   - Select your server

3. **Test in Agent Mode**:
   - Open Chat panel
   - Switch to Agent Mode
   - Try: "Get Sitecore content layout details with ID 12345"

### Manual Testing

For testing outside VS Code:

```bash
# Run the server
dotnet run --project McpSitecoreAiExample.csproj

# In another terminal, send MCP requests
# (You'll need an MCP client or custom test script)
```

[⬆️ Go To Top](#table-of-contents)

## Advanced Features

- **HTTP-Based Servers**: Use `ModelContextProtocol.AspNetCore` for web-based MCP servers
- **Custom Transports**: Implement your own communication protocols
- **Tool Discovery**: Automatic registration of tools from assemblies
- **Error Handling**: Robust error management and logging
- **Security**: Authentication and authorization patterns

[⬆️ Go To Top](#table-of-contents)

## Troubleshooting

**Common Issues:**

- **"Tool not found"**: Ensure your tool class has `[McpServerToolType]` and methods have `[McpServerTool]`
- **Connection errors**: Check your `.vscode/mcp.json` configuration
- **SDK version conflicts**: Use `--prerelease` flag for latest beta versions

**Debug Tips:**

- Enable detailed logging: `builder.Logging.SetMinimumLevel(LogLevel.Debug);`
- Test with simple tools first before complex integrations

[⬆️ Go To Top](#table-of-contents)

## Contributing

We love contributions! Here's how you can help:

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/amazing-enhancement`
3. Make your changes and add tests
4. Submit a pull request

Please read our [Contributing Guidelines](#contributing-guide) for detailed information.

[⬆️ Go To Top](#table-of-contents)

## Contributing Guide

To contribute to this project, follow these simple steps:

1. **Create a feature branch from main**:
   ```bash
   git checkout main
   git pull origin main
   git checkout -b feature/your-feature-name
   ```

2. **Make your changes** and commit them with clear, descriptive messages

3. **Push your branch** to the remote repository:
   ```bash
   git push origin feature/your-feature-name
   ```

4. **Open a Pull Request (PR)** on GitHub:
   - Navigate to the repository
   - Click "New Pull Request"
   - Select your feature branch
   - Add a clear title and description of your changes
   - Submit the PR for review

5. **Address review feedback** if requested by maintainers

6. Once approved, your PR will be **merged into main**

Thank you for contributing to the MCP .NET Tutorial project!

[⬆️ Go To Top](#table-of-contents)

## License

This project is licensed under the Apache License 2.0 - see the [LICENSE](LICENSE) file for details.

[⬆️ Go To Top](#table-of-contents)

## Acknowledgments

- [Model Context Protocol](https://modelcontextprotocol.io/) - The open protocol powering AI tool integration
- [Microsoft C# SDK](https://github.com/modelcontextprotocol/csharp-sdk) - Official SDK implementation
- [SitecoreAI](https://www.sitecore.com/products/ai) - AI-powered content management platform

[⬆️ Go To Top](#table-of-contents)

## Additional Resources

- [Official MCP Documentation](https://modelcontextprotocol.io/)
- [C# SDK API Reference](https://csharp.sdk.modelcontextprotocol.io/)
- [Sitecore Headless Services](https://doc.sitecore.com/xp/en/developers/hd/latest/sitecore-headless-services/index.html)
- [Gaurav Nandankar - GitHub](https://github.com/iamgauravn/mcp_server/tree/main)

[⬆️ Go To Top](#table-of-contents)

---

**Ready to build your own MCP tools?** Star this repo and start creating! If you have questions, open an issue or join the discussion.

*#MCP #CSharp #DotNet #SitecoreAI #Tutorial #AI #ModelContextProtocol #DeveloperTools #ContentManagement*
