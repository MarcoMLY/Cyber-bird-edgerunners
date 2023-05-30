using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Data/DataInt")]
    public class DataInt : ScriptableObject
    {
        [SerializeField] public int Variable;

        public void SetData(int variable)
        {
            Variable = variable;
        }

        public void Add(int variable)
        {
            Variable += variable;
        }
    }
}
