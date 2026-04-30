using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

public partial struct BoidMoveSystem : ISystem
{
    private EntityQuery _boidQuery;

    public void OnCreate(ref SystemState state)
    {
        _boidQuery = state.GetEntityQuery(ComponentType.ReadOnly<LocalTransform>(), ComponentType.ReadOnly<Boid>());
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var settings = SystemAPI.GetSingleton<BoidSettings>();
        var boidQuery = SystemAPI.QueryBuilder().WithAll<LocalTransform, Boid>().Build();
        int boidCount = boidQuery.CalculateEntityCount();
        var cellMap = new NativeParallelMultiHashMap<int, BoidData>(boidCount, Allocator.TempJob);

        var hashJob = new HashPositionsJob
        {
            CellSize = settings.CellSize,
            ParallelMap = cellMap.AsParallelWriter()
        };

        state.Dependency = hashJob.ScheduleParallel(state.Dependency);

        var moveJob = new BoidMoveJob
        {
            DeltaTime = SystemAPI.Time.DeltaTime,
            Settings = settings,
            CellMap = cellMap
        };
        state.Dependency = moveJob.ScheduleParallel(state.Dependency);

        cellMap.Dispose(state.Dependency);
    }
}
