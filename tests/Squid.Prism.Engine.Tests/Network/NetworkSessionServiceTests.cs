using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;
using Moq;
using Squid.Prism.Network.Interfaces.Sessions;
using Squid.Prism.Network.Services;

namespace Squid.Prism.Engine.Tests.Network;

public class NetworkSessionServiceTests
{
    private NetworkSessionService _service;

    [SetUp]
    public void SetUp()
    {
        _service = new NetworkSessionService(new FakeLogger<NetworkSessionService>());
    }

    [Test]
    public void AddSession_ShouldAddSession()
    {
        var sessionId = "session1";
        var sessionObject = new Mock<ISessionObject>().Object;

        _service.AddSession(sessionId, sessionObject);

        Assert.That(1, Is.EqualTo(_service.SessionCount));
        Assert.That(sessionObject, Is.EqualTo(_service.GetSessionObject(sessionId)));
    }

    [Test]
    public void AddSession_ShouldLogWarningIfSessionExists()
    {
        var sessionId = "session1";
        var sessionObject = new Mock<ISessionObject>().Object;

        _service.AddSession(sessionId, sessionObject);
        _service.AddSession(sessionId, sessionObject);

        Assert.That(1, Is.EqualTo(_service.SessionCount));
    }

    [Test]
    public void RemoveSession_ShouldRemoveSession()
    {
        var sessionId = "session1";
        var sessionObject = new Mock<ISessionObject>().Object;

        _service.AddSession(sessionId, sessionObject);
        _service.RemoveSession(sessionId);

        Assert.That(0, Is.EqualTo(_service.SessionCount));
    }

    [Test]
    public void UpdateLastActive_ShouldUpdateLastActive()
    {
        var sessionId = "session1";
        var sessionObject = new Mock<ISessionObject>();
        sessionObject.SetupProperty(s => s.LastActive);

        _service.AddSession(sessionId, sessionObject.Object);
        _service.UpdateLastActive(sessionId);

        Assert.That(DateTime.UtcNow, Is.EqualTo(sessionObject.Object.LastActive).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void GetExpiredSessions_ShouldReturnExpiredSessions()
    {
        var sessionId = "session1";
        var sessionObject = new Mock<ISessionObject>();
        sessionObject.SetupProperty(s => s.LastActive, DateTime.UtcNow.AddMinutes(-10));

        _service.AddSession(sessionId, sessionObject.Object);
        var expiredSessions = _service.GetExpiredSessions(TimeSpan.FromMinutes(5));

        Assert.That(1, Is.EqualTo(expiredSessions.Count()));
    }
}
