using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public bool Origional;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectOfType<DontDestroyOnLoad>().Origional)
        {
            Destroy(gameObject);
        }
        Origional = true;
    }
}
