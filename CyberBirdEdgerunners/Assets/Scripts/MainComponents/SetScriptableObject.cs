using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class SetScriptableObject : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private DataGameObject _data;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _data.SetData(_gameObject);
    }
}
