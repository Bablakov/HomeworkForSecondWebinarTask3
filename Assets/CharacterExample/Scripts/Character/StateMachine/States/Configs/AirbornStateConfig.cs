using System;
using UnityEngine;

[Serializable]
public class AirbornStateConfig 
{
    [SerializeField] private JumpingStateConfig _jumpingStateConfig;
    [field: SerializeField, Range(0, 10)] public float Speed { get; private set; }    

    public JumpingStateConfig JumpingStateConfig => _jumpingStateConfig;

    public float BaseGravity 
        => 2f * _jumpingStateConfig.MaxHeight / (_jumpingStateConfig.TimeToReachMaxHeight * _jumpingStateConfig.TimeToReachMaxHeight);
}
