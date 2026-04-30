using Unity.Entities;
using UnityEngine;

public class BoidSettingsAuthoring : MonoBehaviour
{
    [Min(float.Epsilon)] public float SeparationWeight = 20f;
    [Min(float.Epsilon)] public float AlignmentWeight = 5f;
    [Min(float.Epsilon)] public float CohesionWeight = 2f;
    [Min(float.Epsilon)] public float CellSize = 1f;
    [Min(float.Epsilon)] public float BoundaryRadius = 20f;
    [Min(float.Epsilon)] public float ReturnStrength = 5f;
    [Min(float.Epsilon)] public float OrbitRadius = 35f;
    [Min(float.Epsilon)] public float OrbitSpeed = 0.5f;
    public Vector3 OrbitCenter = Vector3.zero;

    public class BoidSettingsBaker : Baker<BoidSettingsAuthoring>
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
                ReturnStrength = authoring.ReturnStrength,
                OrbitRadius = authoring.OrbitRadius,
                OrbitSpeed = authoring.OrbitSpeed,
                OrbitCenter = authoring.OrbitCenter
            });
        }
    }
}