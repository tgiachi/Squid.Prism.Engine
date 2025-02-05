using System.Diagnostics;
using Humanizer;
using Serilog;
using Squid.Prism.Engine.Core.Interfaces.Listeners;
using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Network.Server.Events;
using Squid.Prism.Server.Core.Events.Scheduler;
using Squid.Prism.Server.Core.Interfaces.Services;
using Squid.Prism.Server.Engine.Data.Directories;

namespace Squid.Prism.Server.Engine.Services;

public class DiagnosticSystemService
    : IDiagnosticSystemService, IEventBusListener<ServerStartedEvent>, IEventBusListener<ServerStoppingEvent>
{
    private readonly ILogger _logger = Log.Logger.ForContext<DiagnosticSystemService>();
    private readonly IEventBusService _eventBusService;

    public string PidFileName { get; }

    private const int _printInterval = 120;

    private int _printCounter;


    public DiagnosticSystemService(IEventBusService eventBusService, DirectoriesConfig directoriesConfig)
    {
        _eventBusService = eventBusService;
        PidFileName = Path.Combine(directoriesConfig.Root, "prismserver.pid");
    }

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        _eventBusService.Subscribe<ServerStartedEvent>(this);
        _eventBusService.Subscribe<ServerStoppingEvent>(this);

        await _eventBusService.PublishAsync(
            new AddSchedulerJobEvent("PrintDiagnosticInfo", TimeSpan.FromMinutes(1), PrintDiagnosticInfoAsync)
        );
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        if (File.Exists(PidFileName))
        {
            File.Delete(PidFileName);
        }

        return Task.CompletedTask;
    }

    public Task OnEventAsync(ServerStartedEvent message)
    {
        File.WriteAllText(PidFileName, Environment.ProcessId.ToString());

        return Task.CompletedTask;
    }

    private Task PrintDiagnosticInfoAsync()
    {
        using var currentProcess = Process.GetCurrentProcess();

        _logger.Information(
            "Memory usage private: {Private} Paged: {Paged} Total Threads: {Threads} PID: {Pid}",
            currentProcess.WorkingSet64.Bytes(),
            GC.GetTotalMemory(false).Bytes(),
            currentProcess.Threads.Count,
            currentProcess.Id
        );

        _printCounter++;

        if (_printCounter % _printInterval == 0)
        {
            _logger.Information("GC Memory: {Memory}", GC.GetTotalMemory(false).Bytes());
            _printCounter = 0;
        }


        return Task.CompletedTask;
    }

    public Task OnEventAsync(ServerStoppingEvent message)
    {
        if (File.Exists(PidFileName))
        {
            File.Delete(PidFileName);
        }

        return Task.CompletedTask;
    }
}
