using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, ISlowDownable
{
    private Rigidbody2D _rb;
    [field:SerializeField] public float NormalSpeed { get; set; }
    public float CurrentSpeed { get; set; }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        CurrentSpeed = NormalSpeed;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        MoveFoward();
    }

    public void ChangeSpeed(float speed)
    {
        CurrentSpeed = NormalSpeed * speed;
    }

    public void ResetSpeed()
    {
        CurrentSpeed = NormalSpeed;
    }

    public virtual void MoveFoward()
    {
        SetVelocity(transform.up.normalized * CurrentSpeed);
    }

    private void SetVelocity(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }
}
