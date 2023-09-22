using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
public struct MovementData : IComponentData
{
    public float speed;

    public float amplitude;
}

public struct PositionData : IComponentData
{
    public float3 position;
}

public class SampleAuthoring : MonoBehaviour
{
    public float authSpeed;

    public float authAmp;

    public float3 authPos;
}
public class SimpleBaker : Baker<SampleAuthoring>
{
    // Start is called before the first frame update
    public override void Bake(SampleAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        
        AddComponent(entity, new MovementData
        {
            speed = authoring.authSpeed,
            amplitude = authoring.authAmp
        });
        AddComponent(entity, new PositionData
        {
            position = authoring.authPos
        });
        
    }
}
