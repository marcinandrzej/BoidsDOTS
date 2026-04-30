using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct HashPositionsJob : IJobEntity
{
    public float CellSize;
    public NativeParallelMultiHashMap<int, BoidData>.ParallelWriter ParallelMap;

    public void Execute(in LocalTransform transform, in Boid boid)
    {
        int3 cellPos = (int3)math.floor(transform.Position / CellSize);
        int key = (int)math.hash(cellPos);

        ParallelMap.Add(key, new BoidData
        {
            Position = transform.Position,
            Velocity = boid.Velocity
        });
    }
}
