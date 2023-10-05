using System;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Week4
{
    [UpdateInGroup(typeof(LateSimulationSystemGroup))]
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



            var job = new BallMovementJob()
            {
                dt = dt,
                moveUp = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow),
                moveDown = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow),
                moveLeft = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow),
                moveRight = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)
                
            };
            job.Schedule();
        }
    }
    [WithAll(typeof(BallMovement))]
    [BurstCompile]
    public partial struct BallMovementJob : IJobEntity
    {
        public float dt;
        
        //key inputs
        public bool moveUp;
        public bool moveDown;
        public bool moveLeft;
        public bool moveRight;

        void Execute(ref LocalTransform transform, in Ball ballData)
        {
            if (moveUp)
            {
                transform.Position.z += (dt * ballData.speed) * ballData.amp;
            }

            if (moveRight)
            {
                transform.Position.x += (dt * ballData.speed) * ballData.amp;
            }

            if (moveLeft)
            {
                transform.Position.x -= (dt * ballData.speed) * ballData.amp;
            }

            if (moveDown)
            {
                transform.Position.z -= (dt * ballData.speed) * ballData.amp;
            }
        }
    }
}