using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private UnityEvent _onFollow;

    private void Start()
    {
        // Set the position of the UI image to match the position of the target game object
        transform.position = _transform.position;

        // Set the rotation of the UI image to match the rotation of the target game object
        transform.rotation = _transform.rotation;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        // Set the position of the UI image to match the position of the target game object
        transform.position = _transform.position;

        // Set the rotation of the UI image to match the rotation of the target game object
        transform.rotation = _transform.rotation;
    }
}
