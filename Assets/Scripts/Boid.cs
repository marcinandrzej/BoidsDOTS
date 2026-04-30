using Unity.Entities;
using Unity.Mathematics;

public struct Boid : IComponentData
{
    public float3 Velocity;
    public float MaxSpeed;
    public float PerceptionRadius;
}
