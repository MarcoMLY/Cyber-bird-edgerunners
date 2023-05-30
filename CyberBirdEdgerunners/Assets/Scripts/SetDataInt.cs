using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Data;

public class SetDataInt : MonoBehaviour
{
    [SerializeField] private DataInt _data;
    [SerializeField] private int _variable;

    [SerializeField] private UnityEvent _onStart;

    // Start is called before the first frame update
    void Start()
    {
        _onStart?.Invoke();
    }

    // Update is called once per frame
    public void SetData()
    {
        _data.SetData(_variable);
    }
}
