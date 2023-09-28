using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Week4
{
    [UpdateBefore(typeof(BallMovementSystem))]
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct ZoneSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<WinZone>();
            state.RequireForUpdate<Config>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var config = SystemAPI.GetSingleton<Config>();
            float radius = config.safeZoneRadius;

            // Debug rendering (the white circle).
            const float debugRenderStepInDegrees = 20;
            for (float angle = 0; angle < 360; angle += debugRenderStepInDegrees)
            {
                
                var a = float3.zero;
                var b = float3.zero;
                math.sincos(math.radians(angle), out a.x, out a.z);
                math.sincos(math.radians(angle + debugRenderStepInDegrees), out b.x, out b.z);
                Debug.DrawLine(a * radius, b * radius);
            }

            
            
            var count = 0;
            foreach (var (transform, ballData) in SystemAPI.Query<RefRO<LocalTransform>, RefRO<Ball>>()
                         .WithAll<BallMovement>())
            {
                count++;
            }

            if (count == 0) return;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            var ball = new NativeArray<Entity>(1, Allocator.Temp);
            ecb.Instantiate(config.ballPrefab, ball);
            ecb.Playback(state.EntityManager);
            var safeZoneJob = new SafeZoneJob
            {
                SquaredRadius = radius * radius
            };
            safeZoneJob.ScheduleParallel();
        }
    }

    [WithAll(typeof(Ball))]
    [WithOptions(EntityQueryOptions.IgnoreComponentEnabledState)]
    [BurstCompile]
    public partial struct SafeZoneJob : IJobEntity
    {
        public float SquaredRadius;

        // Because we want the global position of a child entity, we read LocalToWorld instead of LocalTransform.
        void Execute(in LocalToWorld transformMatrix, EnabledRefRW<BallMovement> movementState)
        {
            movementState.ValueRW = math.lengthsq(transformMatrix.Position) > SquaredRadius;
            
        }
    }
}