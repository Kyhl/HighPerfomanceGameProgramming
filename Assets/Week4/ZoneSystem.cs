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

            count = state.GetEntityQuery((typeof(BallMovement))).CalculateEntityCount();

            if (count == 0)
            {
                var ecb = new EntityCommandBuffer(Allocator.Temp);
                var ball = new NativeArray<Entity>(1, Allocator.Temp);
                ecb.Instantiate(config.ballPrefab, ball);
                ecb.Playback(state.EntityManager);
            }

            var safeZoneJob = new SafeZoneJob
            {
                SquaredRadius = radius * radius
            };
            safeZoneJob.ScheduleParallel();
            // var safeZoneJobHandle = safeZoneJob.Schedule(state.Dependency);

            // var sZJ2 = new TestJob();
            // var jobhandle2 = sZJ2.Schedule(safeZoneJobHandle);
            //
            // state.Dependency = jobhandle2;
        }
    }

    [WithAll(typeof(Ball))]
    [WithOptions(EntityQueryOptions.IgnoreComponentEnabledState)]
    [BurstCompile]
    public partial struct SafeZoneJob : IJobEntity
    {
        public float SquaredRadius;

        // Because we want the global position of a child entity, we read LocalToWorld instead of LocalTransform.
        void Execute(ref LocalTransform transform, EnabledRefRW<BallMovement> movementState,in LocalToWorld transformMatrix)
        {
            var check = math.lengthsq(transformMatrix.Position) > SquaredRadius;
            movementState.ValueRW = check;
            if(!check)transform.Position.x= 7.00f;
        }
    }
    [WithAll(typeof(Ball))]
    [WithOptions(EntityQueryOptions.IgnoreComponentEnabledState)]
    [BurstCompile]
    public partial struct TestJob : IJobEntity
    {

        // Because we want the global position of a child entity, we read LocalToWorld instead of LocalTransform.
        void Execute(ref LocalTransform transform)
        {

            transform.Position.x -= 0.005f;
        }
    }
}