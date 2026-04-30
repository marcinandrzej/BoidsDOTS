using UnityEngine;
using Unity.Entities;

public class BoidSpawnerAuthoring : MonoBehaviour
{
    public GameObject BoidPrefab;
    [Min(1)] public int Count = 1000;
    [Min(float.Epsilon)] public float SpawnRadius = 10f;

    public class BoidSpawnerBaker : Baker<BoidSpawnerAuthoring>
    {
        public override void Bake(BoidSpawnerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new BoidSpawner
            {
                Prefab = GetEntity(authoring.BoidPrefab, TransformUsageFlags.Dynamic),
                Count = authoring.Count,
                SpawnRadius = authoring.SpawnRadius
            });
        }
    }
}
