using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCollision : MonoBehaviour
{
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void EnableAndDisableCollision()
    {
        if (_collider.enabled)
        {
            _collider.enabled = false;
            return;
        }
        _collider.enabled = true;
    }
}
