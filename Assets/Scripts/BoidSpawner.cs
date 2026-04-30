using Unity.Entities;

public struct BoidSpawner : IComponentData
{
    public Entity Prefab;
    public int Count;
    public float SpawnRadius;
}
