using Unity.Entities;
using UnityEngine;

namespace Week4
{
    public class ConfigAuthoring : MonoBehaviour
    {
        public GameObject ballPrefab;
      
        public float safeZoneRadius;

        class Baker : Baker<ConfigAuthoring>
        {
            public override void Bake(ConfigAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new Config
                {
                    ballPrefab = GetEntity(authoring.ballPrefab, TransformUsageFlags.Dynamic),

                    safeZoneRadius = authoring.safeZoneRadius
                });
            }
        }
    }

    public struct Config : IComponentData
    {
        public Entity ballPrefab;

        public float safeZoneRadius;   // Used in a later step.
    }
}