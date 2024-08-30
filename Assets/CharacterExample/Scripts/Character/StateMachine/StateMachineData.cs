using System;
using UnityEngine;

public class StateMachineData
{
    public float XVelocity;
    public float YVelocity;

    private float _speed;
    private float _xInput;

    public float XInput
    {
        get => _xInput;
        set
        {
            if(_xInput < -1 || _xInput > 1)
                throw new ArgumentOutOfRangeException(nameof(_xInput));   

            _xInput = value;
        }
    }

    public float Speed
    {
        get => _speed;
        set
        {
            if(value < 0)
                throw new ArgumentOutOfRangeException(nameof(_speed));

            _speed = value;
        }
    }
}
