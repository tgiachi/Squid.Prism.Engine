using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Noise;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("noise")]
public class NoiseModule
{
    [ScriptFunction("two", "Get 2D noise")]
    public float GetNoise2d(float x, float y, int seed = 1334)
    {
        var fastNoise = new FastNoiseLite(seed);

        return fastNoise.GetNoise(x, y);
    }

    [ScriptFunction("three", "Get 3D noise")]
    public float GetNoise3d(float x, float y, float z, int seed = 1334)
    {
        var fastNoise = new FastNoiseLite(seed);
        return fastNoise.GetNoise(x, y, z);
    }
}
