using System;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Week4
{
    public partial struct BallMovementSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BallMovement>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var dt = SystemAPI.Time.DeltaTime;

            foreach (var (transform, ballData,entity) in SystemAPI.Query<RefRW<LocalTransform>,RefRO<Ball>>().WithEntityAccess())
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    transform.ValueRW.Position.z += (dt * ballData.ValueRO.speed) * ballData.ValueRO.amp;
                }
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
                {
                    transform.ValueRW.Position.x += (dt * ballData.ValueRO.speed) * ballData.ValueRO.amp;
                }
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
                {
                    transform.ValueRW.Position.x -= (dt * ballData.ValueRO.speed) * ballData.ValueRO.amp;
                }
                if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    transform.ValueRW.Position.z -= (dt * ballData.ValueRO.speed) * ballData.ValueRO.amp;
                }
            }
            
        }
    }
}