using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] private DataGameObject _data;
    // Start is called before the first frame update
    void Awake()
    {
        _data.SetData(gameObject);
    }
}
