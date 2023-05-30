using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class AddToDataList : MonoBehaviour
{
    [SerializeField] private DataGameObjectList _data;
    private void OnEnable()
    {
        _data.AddData(gameObject);
    }

    private void OnDisable()
    {
        _data.Remove(gameObject);
    }
}
