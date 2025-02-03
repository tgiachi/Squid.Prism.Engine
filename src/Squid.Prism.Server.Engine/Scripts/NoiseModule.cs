using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Core.Noise;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("noise")]
public class NoiseModule
{
    [ScriptFunction("get_noise_2d")]
    public float GetNoise2d(float x, float y, int seed = 1334)
    {
        var fastNoise = new FastNoiseLite(seed);

        return fastNoise.GetNoise(x, y);
    }

    [ScriptFunction("get_noise_3d")]
    public float GetNoise3d(float x, float y, float z, int seed = 1334)
    {
        var fastNoise = new FastNoiseLite(seed);

        return fastNoise.GetNoise(x, y, z);
    }
}
