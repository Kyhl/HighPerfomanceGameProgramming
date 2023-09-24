using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Week3
{


    public class MovementAuthoring : MonoBehaviour
    {
        public float authSpeed;

        public float authAmp;

        public float3 authPos;


        public class SimpleBaker : Baker<MovementAuthoring>
        {
            // Start is called before the first frame update
            public override void Bake(MovementAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new MovementData
                {
                    speed = 2f,
                    amplitude = 2f
                });
                // AddComponent(entity, new PositionData
                // {
                //     position = authoring.authPos
                // });

            }
        }
    }

    public struct MovementData : IComponentData, IEnableableComponent
    {
        public float speed;

        public float amplitude;
    }

    public struct PositionData : IComponentData
    {
        public float3 position;
    }
}