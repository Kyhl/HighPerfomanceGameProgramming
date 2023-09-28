using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

namespace Week4
{
    public class BallAuthoring : MonoBehaviour
    {
        

        class Baker : Baker<BallAuthoring>
        {
            public override void Bake(BallAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new Ball()
                {
                    speed = 1.5f,
                    amp = 1f
                });
                
            }
        }
    }

    public struct Ball : IComponentData
    {
        public float speed;

        public float amp;
    }
    
}
