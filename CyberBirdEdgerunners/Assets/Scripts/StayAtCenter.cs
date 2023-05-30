using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class StayAtCenter : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        transform.position = Vector2.zero;
    }

    private void Start()
    {
        transform.position = Vector2.zero;
    }

    private void OnEnable()
    {
        transform.position = Vector2.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.zero;
    }
}
