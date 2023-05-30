using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Data/DataIntList")]
    public class DataIntList : ScriptableObject
    {
        [HideInInspector] public List<int> Variable = new List<int>();

        public int[] GetData()
        {
            return Variable.ToArray();
        }

        public void Clear()
        {
            Variable.Clear();
        }

        public void SetData(List<int> variable)
        {
            Variable = variable;
        }

        public void AddData(int variable)
        {
            Variable.Add(variable);
        }

        public void Remove(int variable)
        {
            Variable.Remove(variable);
        }
    }
}