using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlowDownable
{
    public float CurrentSpeed { get; set; }
    public float NormalSpeed { get; set; }

    void ChangeSpeed(float speed);

    void ResetSpeed();
}
