using Unity.Entities;
using Unity.Mathematics;

public struct BoidSettings : IComponentData
{
    public float SeparationWeight;
    public float AlignmentWeight;
    public float CohesionWeight;
    public float CellSize;
    public float BoundaryRadius;
    public float ReturnStrength;
    public float3 OrbitCenter;
    public float OrbitRadius;
    public float OrbitSpeed;
}
