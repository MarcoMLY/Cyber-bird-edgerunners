using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    [HideInInspector] public List<float> Timers = new List<float>();
    [HideInInspector] public List<Action> Actions = new List<Action>();

    // Update is called once per frame
    void Update()
    {
        for (int i = Timers.Count - 1; i >= 0; i--)
        {
            Timers[i] -= Time.unscaledDeltaTime;
            if (Timers[i] > 0)
            {
                continue;
            }
            Timers.Remove(Timers[i]);
            Actions[i]?.Invoke();
            Actions.Remove(Actions[i]);
        }
    }

    public void StartTimer(float time, Action action)
    {
        Timers.Add(time);
        Actions.Add(action);
    }

    public void RemoveTimer(Action action)
    {
        if (!Actions.Contains(action))
        {
            return;
        }

        int count = 0;
        for (int i = Timers.Count - 1; i >= 0; i--)
        {
            if (Actions[i] != action)
            {
                continue;
            }
            count = i;
        }
        Actions.Remove(action);
        Timers.RemoveAt(count);
    }

    public bool FindTimer(Action action)
    {
        return Actions.Contains(action);
    }
}
