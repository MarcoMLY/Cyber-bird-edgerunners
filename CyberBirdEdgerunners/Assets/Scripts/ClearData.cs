using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class ClearData : MonoBehaviour
{
    public List<DataIntList> DataIntListVars = new List<DataIntList>();
    public List<DataGameObjectList> DataGameObjectListVars = new List<DataGameObjectList>();

    // Start is called before the first frame update
    void OnDisable()
    {
        foreach (DataIntList DataIntListVar in DataIntListVars)
        {
            DataIntListVar.Clear();
        }

        foreach (DataGameObjectList DataGameObjectListVar in DataGameObjectListVars)
        {
            DataGameObjectListVar.Clear();
        }
    }
}
