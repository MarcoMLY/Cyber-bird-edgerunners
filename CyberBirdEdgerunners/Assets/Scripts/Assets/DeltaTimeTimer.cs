using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeltaTimeTimer : Timer
{
    // Update is called once per frame
    void Update()
    {
        for (int i = Timers.Count - 1; i >= 0; i--)
        {
            Timers[i] -= Time.deltaTime;
            if (Timers[i] > 0)
            {
                continue;
            }
            Timers.Remove(Timers[i]);
            Actions[i]?.Invoke();
            Actions.Remove(Actions[i]);
        }
    }
}
