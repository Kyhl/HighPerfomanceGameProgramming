using System.Runtime.InteropServices;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Week4
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
    public partial struct CameraSystem : ISystem
    {
        Entity target;
        Random random;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerCamera>();
            random = new Random(123);
        }

        // Because this OnUpdate accesses managed objects, it cannot be Burst-compiled.
        public void OnUpdate(ref SystemState state)
        {
            if (target == Entity.Null || Input.GetKeyDown(KeyCode.Space))
            {
                var ballQuery = SystemAPI.QueryBuilder().WithAll<Ball>().Build();
                var balls = ballQuery.ToEntityArray(Allocator.Temp);
                if (balls.Length == 0)
                {
                    return;
                }
            
                target = balls[random.NextInt(balls.Length)];
            }
            
            var cameraTransform = CameraSingleton.instance.transform;
            var ballTransform = SystemAPI.GetComponent<LocalToWorld>(target);
            Vector3 position = ballTransform.Position;
            position -= 10.0f * (Vector3)ballTransform.Forward;  // move the camera back from the tank
            position += new Vector3(0, 5f, 0);  // raise the camera by an offset
            cameraTransform.position = position;
            cameraTransform.LookAt(ballTransform.Position);
        }
    }
}
