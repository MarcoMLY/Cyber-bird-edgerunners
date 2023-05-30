using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/Data/DataGameObject")]
    public class DataGameObject : ScriptableObject
    {
        [HideInInspector] public GameObject Variable;

        public void SetData(GameObject variable)
        {
            Variable = variable;
        }
    }
}
