using Unity.Entities;
using UnityEngine;

namespace Week4
{
    public class MainAuthoring : MonoBehaviour
    {
        public bool PlayerCamera;

        public bool BallMovement;

        class Baker : Baker<MainAuthoring>
        {
            public override void Bake(MainAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
            
                if (authoring.PlayerCamera) AddComponent<PlayerCamera>(entity);
                if (authoring.BallMovement) AddComponent<BallMovement>(entity);
            }
        }
    }

    public struct PlayerCamera : IComponentData
    {
    }
    public struct BallMovement : IComponentData
    {
    }
}