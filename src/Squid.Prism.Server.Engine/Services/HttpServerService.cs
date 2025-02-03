using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Serilog;
using Squid.Prism.Engine.Core.Configs;
using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Data.Configs;
using Squid.Prism.Server.Core.Extensions;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Types;

using Squid.Prism.Server.Engine.Data.Directories;
using Squid.Prism.Server.Engine.Types;
using WatsonWebserver;
using WatsonWebserver.Core;
using HttpMethod = WatsonWebserver.Core.HttpMethod;


namespace Squid.Prism.Server.Engine.Services;

public partial class HttpServerService : IHttpServerService
{
    private Webserver _server;
    private readonly DirectoriesConfig _directoriesConfig;

    [ConfigVariable] public HttpServerConfig Config { get; set; }
    private readonly ILogger _logger = Log.ForContext<HttpServerService>();

    private readonly IVariablesService _variablesService;
    private bool _isRunning;

    public HttpServerService(
        DirectoriesConfig directoriesConfig, IVariablesService variablesService, IEventBusService eventBusService
    )
    {
        _directoriesConfig = directoriesConfig;
        _variablesService = variablesService;
    }

    private void InitializeHttpDirectory()
    {
        var httpPath = _directoriesConfig[DirectoryType.Http];
        _logger.Debug("Initializing HTTP directory at {Path}", httpPath);


        _server.Routes.PreAuthentication.Dynamic.Add(
            HttpMethod.GET,
            RootRegex(),
            async (ctx) =>
            {
                var requestPath = ctx.Request.Url.RawWithoutQuery.TrimStart('/');
                var fullPath = string.IsNullOrEmpty(requestPath)
                    ? Path.Combine(httpPath, "index.html")
                    : Path.Combine(httpPath, requestPath);


                if (Directory.Exists(fullPath))
                {
                    fullPath = Path.Combine(fullPath, "index.html");
                }

                var (content, contentType, statusCode) = await ReadFromFileSystem(fullPath);

                ctx.Response.StatusCode = statusCode;
                ctx.Response.ContentType = contentType;
                await ctx.Response.Send(content);
            }
        );
    }

    public void AddContentRoute(string path, bool listFiles)
    {
        _logger.Debug("Adding content route for path {Path} (listFiles: {ListFiles})", path, listFiles);
        _server.Routes.PreAuthentication.Content.Add(path, listFiles);
    }

    public void AddStaticRoute(RouteMethodType method, string path, Func<HttpContextBase, Task> handler)
    {
        _logger.Debug("Adding static route {Method} {Path}", method, path);
        _server.Routes.PreAuthentication.Static.Add(
            method.ToHttpMethod(),
            path,
            async (ctx) =>
            {
                try
                {
                    await handler(ctx);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error in static route {Method} {Path}", method, path);
                    ctx.Response.StatusCode = 500;
                    await ctx.Response.Send("Internal Server Error");
                }
            }
        );
    }

    public void AddParameterRoute(RouteMethodType method, string path, Func<HttpContextBase, Task> handler)
    {
        _logger.Debug("Adding parameter route {Method} {Path}", method, path);
        _server.Routes.PreAuthentication.Parameter.Add(
            method.ToHttpMethod(),
            path,
            async (ctx) =>
            {
                try
                {
                    await handler(ctx);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error in parameter route {Method} {Path}", method, path);
                    ctx.Response.StatusCode = 500;
                    await ctx.Response.Send("Internal Server Error");
                }
            }
        );
    }

    private async Task<(byte[] Content, string ContentType, int StatusCode)> ReadFromFileSystem(string fileName)
    {
        try
        {
            if (!File.Exists(fileName))
            {
                _logger.Warning("File not found: {FileName}", fileName);
                return ("404 - File Not Found"u8.ToArray(), "text/plain", 404);
            }

            string contentType = GetContentType(fileName);
            byte[] content;

            if (contentType == "text/html")
            {
                var htmlContent = await File.ReadAllTextAsync(fileName);
                var processedContent = _variablesService.TranslateText(htmlContent);
                content = Encoding.UTF8.GetBytes(processedContent);
            }
            else
            {
                content = await File.ReadAllBytesAsync(fileName);
            }

            _logger.Debug("Successfully read file: {FileName}", fileName);
            return (content, contentType, 200);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error reading file: {FileName}", fileName);
            return ("500 - Internal Server Error"u8.ToArray(), "text/plain", 500);
        }
    }


    public static string GetLocalIPViaConnection()
    {
        using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0);
        socket.Connect("8.8.8.8", 65530);
        IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
        return endPoint.Address.ToString();
    }


    public void AddDynamicRoute(RouteMethodType method, Regex pattern, Func<HttpContextBase, Task> handler)
    {
        _logger.Debug("Adding dynamic route {Method} {Pattern}", method, pattern);
        _server.Routes.PreAuthentication.Dynamic.Add(
            method.ToHttpMethod(),
            pattern,
            async (ctx) =>
            {
                try
                {
                    await handler(ctx);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error in dynamic route {Method} {Pattern}", method, pattern);
                    ctx.Response.StatusCode = 500;
                    await ctx.Response.Send("Internal Server Error");
                }
            }
        );
    }


    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _server = new Webserver(new WebserverSettings("127.0.0.1", Config.Port), DefaultRoute);
        InitializeHttpDirectory();

        if (!_isRunning)
        {
            _logger.Information(
                "Starting HTTP server on http://{Hostname}:{Port}",
                _server.Settings.Hostname,
                _server.Settings.Port
            );
            _server.StartAsync(cancellationToken);
            _isRunning = true;
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken = default)
    {
        if (_isRunning)
        {
            _logger.Information("Stopping HTTP server");
            _server.Stop();
            _isRunning = false;
        }
    }


    static async Task DefaultRoute(HttpContextBase ctx) =>
        await ctx.Response.Send("Hello from the default route!");


    private static string GetContentType(string path)
    {
        return Path.GetExtension(path).ToLower() switch
        {
            ".html"           => "text/html",
            ".css"            => "text/css",
            ".js"             => "application/javascript",
            ".json"           => "application/json",
            ".png"            => "image/png",
            ".jpg" or ".jpeg" => "image/jpeg",
            ".gif"            => "image/gif",
            ".svg"            => "image/svg+xml",
            ".ico"            => "image/x-icon",
            _                 => "application/octet-stream"
        };
    }


    [GeneratedRegex("/*")]
    private static partial Regex RootRegex();
}
