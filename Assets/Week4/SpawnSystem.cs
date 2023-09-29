using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Week4
{


    [UpdateAfter(typeof(BallMovementSystem))]
    public partial struct SpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<Spawner>();
            state.RequireForUpdate<Config>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var config = SystemAPI.GetSingleton<Config>();

            var count = 0;
            foreach (var (transform, ballData) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<Ball>>()
                         .WithAll<BallMovement>())
            {
                count++;
            }

            if (count != 0) return;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var ball = new NativeArray<Entity>(1, Allocator.Temp);
           var entity =  ecb.Instantiate(config.ballPrefab);
           ecb.SetComponent(entity, new LocalTransform()
           {
               Scale = 1.0f,
               Position = new float3(10,0.5f,-0.1f),
               Rotation = new quaternion()
           });
            ecb.Playback(state.EntityManager);
        }
    }
}
