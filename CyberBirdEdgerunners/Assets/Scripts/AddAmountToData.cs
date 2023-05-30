using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class AddAmountToData : MonoBehaviour
{
    [SerializeField] private DataInt _dataInt;
    [SerializeField] private int _amount;

    public void AddAmount()
    {
        _dataInt.Add(_amount);
    }
}
