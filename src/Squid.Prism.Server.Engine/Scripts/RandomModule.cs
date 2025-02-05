using Squid.Prism.Server.Core.Attributes.Scripts;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("random")]
public class RandomModule
{
    [ScriptFunction("int", "Get a random integer")]
    public int GetRandomInt(int min, int max)
    {
        return new Random().Next(min, max);
    }

    [ScriptFunction("float", "Get a random float")]
    public float GetRandomFloat(float min, float max)
    {
        return (float)(new Random().NextDouble() * (max - min) + min);
    }

    [ScriptFunction("bool", "Get a random boolean")]
    public bool GetRandomBool()
    {
        return new Random().Next(0, 2) == 1;
    }
}
