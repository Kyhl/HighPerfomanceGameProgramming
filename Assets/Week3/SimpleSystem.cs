using System;
using Unity.Entities;
using Unity.Mathematics;

namespace DefaultNamespace
{
    public partial struct SimpleSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            // Entity entity = state.EntityManager.CreateEntity();
            //
            // state.EntityManager.SetName(entity,"Bomba");
            //
            // state.EntityManager.AddComponentData(entity, new MovementData
            // {
            //     speed = 2f,
            //     amplitude = 4f
            // });
            //
            // state.EntityManager.AddComponentData(entity, new PositionData
            // {
            //     position = new float3(1, 1, 1)
            // });



        }

        public void OnUpdate(ref SystemState state)
        {
            foreach (var (pos, move) in SystemAPI.Query<RefRW<PositionData>, RefRO<MovementData>>())
            {
                pos.ValueRW.position.y = math.sin(((float)SystemAPI.Time.ElapsedTime * move.ValueRO.speed))*move.ValueRO.amplitude;
            }
        }
    }

    // public struct MovementData : IComponentData
    // {
    //     public float speed;
    //
    //     public float amplitude;
    // }
    //
    // public struct PositionData : IComponentData
    // {
    //     public float3 position;
    // }
}