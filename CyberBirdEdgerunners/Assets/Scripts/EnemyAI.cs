using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Data;

public class EnemyAI : MonoBehaviour, ISlowDownable
{
    private Rigidbody2D _rb;

    [field: SerializeField] public float NormalSpeed { get; set; }
    public float CurrentSpeed { get; set; }
    [SerializeField] private float _turnSpeed;
    [SerializeField] private float _moveAwayFromEnemiesSpeed;

    [SerializeField] private DataGameObject _player;

    [SerializeField] private DataGameObjectList _enemyData;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        CurrentSpeed = NormalSpeed;

        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 1);

        foreach (Collider2D collider in hit)
        {
            if (collider.gameObject.CompareTag("Cloud"))
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_player.Variable == null)
            return;
        ChasePlayer();
    }

    private Vector2 GetAwayFromAllEnemiesVector()
    {
        Vector2 direction = new Vector2(0, 0);
        for (int i = 0; i < _enemyData.Variable.Count; i++)
        {
            Transform enemy = _enemyData.Variable[i].transform;
            if (enemy == transform)
                continue;
            float distance = Vector2.Distance(transform.position, enemy.position);
            Vector2 directionToEnemy = enemy.position - transform.position;
            direction += -directionToEnemy * _moveAwayFromEnemiesSpeed / distance;
        }
        return direction;
    }

    public void ChangeSpeed(float speed)
    {
        CurrentSpeed = NormalSpeed * speed;
    }

    public void ResetSpeed()
    {
        CurrentSpeed = NormalSpeed;
    }

    public void ChasePlayer()
    {
        Vector2 direction = _player.Variable.transform.position - transform.position;
        direction.Normalize();
        direction *= CurrentSpeed;
        RotateTowards(direction);
        direction += GetAwayFromAllEnemiesVector();
        SetVelocity(direction);
    }

    private void SetVelocity(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }

    private void RotateTowards(Vector2 direction)
    {
        transform.up = Vector2.Lerp(transform.up, direction, Time.deltaTime * _turnSpeed);
    }
}
