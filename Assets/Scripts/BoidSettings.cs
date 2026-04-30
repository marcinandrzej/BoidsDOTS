using Unity.Entities;

public struct BoidSettings : IComponentData
{
    public float SeparationWeight;
    public float AlignmentWeight;
    public float CohesionWeight;
    public float CellSize;
    public float BoundaryRadius;
    public float ReturnStrength;
}
