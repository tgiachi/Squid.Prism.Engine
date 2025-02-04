using System.Collections.Concurrent;
using System.Numerics;
using Microsoft.Extensions.Logging;
using Squid.Prism.Engine.Core.Configs;
using Squid.Prism.Engine.Core.Interfaces.Services;
using Squid.Prism.Server.Core.Data.Configs;
using Squid.Prism.Server.Core.Data.World;
using Squid.Prism.Server.Core.Data.World.Build;
using Squid.Prism.Server.Core.Interfaces.Services.Game;
using Squid.Prism.Server.Core.Interfaces.World.Builder;

namespace Squid.Prism.Server.Engine.Services.Game;

public class WorldService : IWorldService
{
    [ConfigVariable] public WorldConfig Config { get; set; }

    private const string _name = "world";
    private readonly ConcurrentDictionary<Vector3, ChunkEntity> _chunks = new();


    private readonly Dictionary<byte, BiomeEntity> _biomes = new();

    private readonly ILogger _logger;
    private readonly List<IChunkBuilderPipe> _pipes = new();
    private readonly IProcessQueueService _processQueueService;

    public WorldService(ILogger<WorldService> logger, IProcessQueueService processQueueService)
    {
        _processQueueService = processQueueService;
        _processQueueService.EnsureContext(_name);
        _logger = logger;
    }


    public Task StartAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("World service started with render distance: {Distance}", Config.RenderDistance);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public void AddChunkBuilderPipe(IChunkBuilderPipe pipe)
    {
        _pipes.Add(pipe);
    }

    public async Task<ChunkEntity> GetChunkAsync(Vector3 position)
    {
        if (_chunks.TryGetValue(position, out var chunk))
        {
            return chunk;
        }

        await _processQueueService.Enqueue(
            _name,
            async () =>
            {
                var context = new ChunkBuilderContext(position);
                var result = await ExecuteChunkGenPipelineAsync(context);

                _chunks.TryAdd(position, result.Chunk);
            }
        );

        return await GetChunkAsync(position);
    }

    public void AddBiome(byte id, BiomeEntity biome)
    {
        _logger.LogInformation("Adding biome {Biome} with id {Id}", biome.Name, id);
        _biomes[id] = biome;
    }

    public BiomeEntity GetBiome(byte id)
    {
        return _biomes[id];
    }

    private async Task<ChunkBuilderContext> ExecuteChunkGenPipelineAsync(ChunkBuilderContext context)
    {
        foreach (var pipe in _pipes)
        {
            context = await pipe.ProcessAsync(context);
        }

        return context;
    }
}
