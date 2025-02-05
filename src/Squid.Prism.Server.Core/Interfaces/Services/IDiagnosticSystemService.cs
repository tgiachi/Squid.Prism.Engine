using Squid.Prism.Engine.Core.Interfaces.Services.Base;

namespace Squid.Prism.Server.Core.Interfaces.Services;

public interface IDiagnosticSystemService : ISquidPrismAutostart
{
    string PidFileName { get; }
}
