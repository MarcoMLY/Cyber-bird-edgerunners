using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    [SerializeField] private UnityEventVariables _trigger;

    private void OnEnable()
    {
        _gameEvent.AddListener(this);
    }

    private void OnDisable()
    {
        _gameEvent.RemoveListener(this);
    }

    public void TriggerEvent(Component component, int variable)
    {
        _trigger?.Invoke(component, variable);
    }
}
