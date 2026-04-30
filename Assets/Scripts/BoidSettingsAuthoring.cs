using UnityEngine;
using Unity.Entities;

public class BoidSettingsAuthoring : MonoBehaviour
{
    [Min(float.Epsilon)] public float SeparationWeight = 20f;
    [Min(float.Epsilon)] public float AlignmentWeight = 5f;
    [Min(float.Epsilon)] public float CohesionWeight = 2f;
    [Min(float.Epsilon)] public float CellSize = 1f;
    [Min(float.Epsilon)] public float BoundaryRadius = 20f;
    [Min(float.Epsilon)] public float ReturnStrength = 5f;

    public class BoidSsettingsBaker : Baker<BoidSettingsAuthoring>
    {
        public override void Bake(BoidSettingsAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, new BoidSettings
            {
                SeparationWeight = authoring.SeparationWeight,
                AlignmentWeight = authoring.AlignmentWeight,
                CohesionWeight = authoring.CohesionWeight,
                CellSize = authoring.CellSize,
                BoundaryRadius = authoring.BoundaryRadius,
                ReturnStrength = authoring.ReturnStrength
            });
        }
    }
}