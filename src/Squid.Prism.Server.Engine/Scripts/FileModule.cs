using Squid.Prism.Server.Core.Attributes.Scripts;
using Squid.Prism.Server.Engine.Data.Directories;

namespace Squid.Prism.Server.Engine.Scripts;

[ScriptModule("files")]
public class FileModule
{
    private readonly DirectoriesConfig _directoriesConfig;

    public FileModule(DirectoriesConfig directoriesConfig)
    {
        _directoriesConfig = directoriesConfig;
    }


    [ScriptFunction("load_file", "Load a file")]
    public string LoadFile(string fileName)
    {
        var file = Path.Combine(_directoriesConfig.Root, fileName);

        if (!File.Exists(file))
        {
            throw new FileNotFoundException($"File {fileName} not found");
        }

        return File.ReadAllText(file);
    }

    [ScriptFunction("load_file_as_array", "Load a file as an array")]
    public string[] LoadFileAsArray(string fileName)
    {
        var file = Path.Combine(_directoriesConfig.Root, fileName);

        if (!File.Exists(file))
        {
            throw new FileNotFoundException($"File {fileName} not found");
        }

        return File.ReadAllLines(file);
    }

    [ScriptFunction("write_file", "Write a file")]
    public string WriteFile(string fileName, string content)
    {
        var file = Path.Combine(_directoriesConfig.Root, fileName);

        File.WriteAllText(file, content);

        return file;
    }
}
