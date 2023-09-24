using System;
using System.Diagnostics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Week3
{
    [BurstCompile]
    public partial struct SimpleSystem : ISystem
    {
        [BurstCompile]
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
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            Console.WriteLine("Hi");
            Debug.WriteLine("Hi but in debug");
            foreach (var (pos, move) in SystemAPI.Query<RefRW<PositionData>, RefRO<MovementData>>())
            {
                pos.ValueRW.position.y = math.sin(((float)SystemAPI.Time.ElapsedTime * move.ValueRO.speed))*move.ValueRO.amplitude;
                Console.WriteLine("Hi");
                Debug.WriteLine("Hi but in debug");
            }
            foreach (var (pos, move) in SystemAPI.Query<RefRW<LocalTransform>, RefRO<MovementData>>())
            {
                pos.ValueRW.Position.y = math.sin(((float)SystemAPI.Time.ElapsedTime * move.ValueRO.speed))*move.ValueRO.amplitude;
                Console.WriteLine("Hi");
                Debug.WriteLine("Hi but in debug");
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