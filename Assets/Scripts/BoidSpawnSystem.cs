using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

[WorldSystemFilter(WorldSystemFilterFlags.Default)]
public partial struct BoidSpawnSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        state.Enabled = false;

        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);
        
        foreach (var spawner in SystemAPI.Query<RefRO<BoidSpawner>>())
        {
            uint timeSeed = (uint)System.DateTime.Now.Ticks;
            var random = Random.CreateFromIndex(timeSeed);

            for (int i = 0; i < spawner.ValueRO.Count; i++)
            {
                Entity newBoid = ecb.Instantiate(spawner.ValueRO.Prefab);
                float3 randomPos = random.NextFloat3Direction() * random.NextFloat(0, spawner.ValueRO.SpawnRadius);
                float3 randomDir = random.NextFloat3Direction();
                var transform = LocalTransform.FromPosition(randomPos);
                transform.Rotation = quaternion.LookRotationSafe(randomDir, math.up());
                ecb.SetComponent(newBoid, transform);
            }
        }

        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}
