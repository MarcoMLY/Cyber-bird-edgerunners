using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventListener> GameEventListeners = new List<GameEventListener>();

    public void AddListener(GameEventListener listener)
    {
        if (!GameEventListeners.Contains(listener))
            GameEventListeners.Add(listener);
    }

    public void RemoveListener(GameEventListener listener)
    {
        if (GameEventListeners.Contains(listener))
            GameEventListeners.Remove(listener);
    }

    public void OnTriggerEvent(Component component, int variable)
    {
        for (int i = GameEventListeners.Count - 1; i >= 0; i--)
        {
            GameEventListeners[i].TriggerEvent(component, variable);
        }
    }
}
