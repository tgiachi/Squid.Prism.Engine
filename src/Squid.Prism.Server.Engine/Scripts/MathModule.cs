using Squid.Prism.Server.Core.Attributes.Scripts;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("math_s")]
public class MathModule
{
    [ScriptFunction("lerp", "Linear interpolation")]
    public float Lerp(float min, float max, float num)
    {
        return (min + (max - min) * num);
    }

    [ScriptFunction("clamp", "Clamp a value between min and max")]
    public float Clamp(float value, float min, float max)
    {
        return value < min ? min :
            value > max ? max : value;
    }

    [ScriptFunction("abs", "Absolute value")]
    public float Abs(float value)
    {
        return value < 0 ? -value : value;
    }

    [ScriptFunction("ceil", "Ceil value")]
    public float Ceil(float value)
    {
        return (float)Math.Ceiling(value);
    }

    [ScriptFunction("floor", "Floor value")]
    public float Floor(float value)
    {
        return (float)Math.Floor(value);
    }

    [ScriptFunction("round", "Round value")]
    public float Round(float value)
    {
        return (float)Math.Round(value);
    }

    [ScriptFunction("sqrt", "Square root")]
    public float Sqrt(float value)
    {
        return (float)Math.Sqrt(value);
    }

    [ScriptFunction("max", "Max value")]
    public int Max(int a, int b)
    {
        return Math.Max(a, b);
    }

    [ScriptFunction("min", "Min value")]
    public int Min(int a, int b)
    {
        return Math.Min(a, b);
    }


}
