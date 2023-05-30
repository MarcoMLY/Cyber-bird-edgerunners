using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Data/DataGameObjectList")]
    public class DataGameObjectList : ScriptableObject
    {
        [HideInInspector] public List<GameObject> Variable = new List<GameObject>();

        public GameObject[] GetData()
        {
            return Variable.ToArray();
        }

        public void Clear()
        {
            Variable.Clear();
        }

        public void SetData(List<GameObject> variable)
        {
            Variable = variable;
        }

        public void AddData(GameObject variable)
        {
            Variable.Add(variable);
        }

        public void Remove(GameObject variable)
        {
            Variable.Remove(variable);
        }
    }
}
