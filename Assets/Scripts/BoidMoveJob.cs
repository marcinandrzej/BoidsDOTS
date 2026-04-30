using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
public partial struct BoidMoveJob : IJobEntity
{
    public const float MovingTreshold = 0.001f;

    public float DeltaTime;
    public BoidSettings Settings;
    [ReadOnly] public NativeParallelMultiHashMap<int, BoidData> CellMap;

    public void Execute(ref LocalTransform transform, ref Boid boid)
    {
        int3 centerCell = (int3)math.floor(transform.Position / Settings.CellSize);
        float3 separationSum = 0;
        float3 alignmentSum = 0;
        float3 cohesionSum = 0;
        int neighborsCount = 0;

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    int key = (int)math.hash(centerCell + new int3(x, y, z));

                    if (CellMap.TryGetFirstValue(key, out BoidData other, out var it))
                    {
                        do
                        {
                            float d = math.distance(transform.Position, other.Position);
                            if (d > 0 && d < boid.PerceptionRadius)
                            {
                                separationSum += (transform.Position - other.Position) / d;
                                alignmentSum += other.Velocity;
                                cohesionSum += other.Position;
                                neighborsCount++;
                            }
                        } while (CellMap.TryGetNextValue(out other, ref it));
                    }
                }
            }
        }

        if (neighborsCount > 0)
        {
            float3 avgAlignment = alignmentSum / neighborsCount;
            float3 avgCohesion = (cohesionSum / neighborsCount) - transform.Position;

            boid.Velocity += separationSum * Settings.SeparationWeight * DeltaTime;
            boid.Velocity += (avgAlignment - boid.Velocity) * Settings.AlignmentWeight * DeltaTime;
            boid.Velocity += avgCohesion * Settings.CohesionWeight * DeltaTime;
        }

        float3 dirToCenter = -transform.Position;

        if (math.length(transform.Position) > Settings.BoundaryRadius)
        {
            boid.Velocity += math.normalize(dirToCenter) * Settings.ReturnStrength * DeltaTime;
        }

        if (math.length(boid.Velocity) > boid.MaxSpeed)
        {
            boid.Velocity = math.normalize(boid.Velocity) * boid.MaxSpeed;
        }

        transform.Position += boid.Velocity * DeltaTime;

        if (math.lengthsq(boid.Velocity) > MovingTreshold)
        {
            transform.Rotation = quaternion.LookRotationSafe(boid.Velocity, math.up());
        }
    }
}
