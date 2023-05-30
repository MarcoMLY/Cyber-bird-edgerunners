using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class ChangeEnemySpeed : MonoBehaviour
{
    [SerializeField] private DataGameObjectList _ememyData;
    [SerializeField] private DataGameObjectList _bulletData;
    private float _time;

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

    public void PauseTemporarily(float time)
    {
        ChangeSpeed(0.01f);
        _time = time;
    }

    public void ChangeSpeed(float speed)
    {
        foreach (GameObject enemy in _ememyData.GetData())
        {
            enemy.GetComponent<ISlowDownable>().ChangeSpeed(speed);
        }

        foreach (GameObject bullet in _bulletData.Variable)
        {
            bullet.GetComponent<ISlowDownable>().ChangeSpeed(speed);
        }
    }

    public void ResetSpeed()
    {
        foreach (GameObject enemy in _ememyData.GetData())
        {
            enemy.GetComponent<ISlowDownable>().ResetSpeed();
        }

        foreach (GameObject bullet in _bulletData.Variable)
        {
            bullet.GetComponent<ISlowDownable>().ResetSpeed();
        }
    }
}
