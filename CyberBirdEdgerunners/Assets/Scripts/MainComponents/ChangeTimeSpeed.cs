using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTimeSpeed : MonoBehaviour
{
    private float _normalTimeSteps;
    private float _multiplier;
    private float _time;

    // Start is called before the first frame update
    private void Start()
    {
        _normalTimeSteps = Time.fixedDeltaTime;
        _multiplier = _normalTimeSteps / Time.timeScale;
    }

    private void FixedUpdate()
    {
        if (_time <= 0)
        {
            return;
        }

        _time -= Time.unscaledDeltaTime;
        if (_time <= 0)
        {
            ResetSpeed();
        }
    }

    public void ChangeSpeed(float speed)
    {
        Time.timeScale = speed;
        Time.fixedDeltaTime = speed * _multiplier;
    }

    public void ResetSpeed()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = _normalTimeSteps;
    }

    public void PauseTemporarily(float time)
    {
        ChangeSpeed(0.01f);
        _time = time;
    }
}
