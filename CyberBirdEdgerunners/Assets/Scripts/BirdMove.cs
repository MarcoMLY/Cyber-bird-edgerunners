using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class BirdMove : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private bool _paused = false;

    [SerializeField] private DataGameObject _cameraData;
    private Camera _camera;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _camera = _cameraData.Variable.GetComponent<Camera>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_paused)
        {
            return;
        }
          
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)transform.position;
        direction.Normalize();
        RotateTowards(direction);
        Vector2 velocity = transform.up.normalized;
        SetVelocity(velocity * _speed);
    }

    private void SetVelocity(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }

    private void RotateTowards(Vector2 direction)
    {
        transform.up = Vector2.Lerp(transform.up, direction, Time.deltaTime * _rotationSpeed);
    }

    public void PauseVelocity()
    {
        _paused = true;
    }

    public void UnPauseVelocity()
    {
        _paused = false;
    }
}
