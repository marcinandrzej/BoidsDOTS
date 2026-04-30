using UnityEngine;
using Unity.Entities;

public class BoidAuthoring : MonoBehaviour
{
    [Min(float.Epsilon)] public float MaxSpeed = 5f;
    [Min(float.Epsilon)] public float PerceptionRadius = 2f;

    public class BoidBaker : Baker<BoidAuthoring>
    {
        public override void Bake(BoidAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new Boid
            {
                Velocity = authoring.transform.forward * authoring.MaxSpeed,
                MaxSpeed = authoring.MaxSpeed,
                PerceptionRadius = authoring.PerceptionRadius
            });
        }
    }
}
