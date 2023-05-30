using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private GameObject[] _particles;

    //[SerializeField] private float _rotation;

    [Header("Particle Settings")]
    [Range(0, 360)]
    [SerializeField] private float _angle;
    [Range(0, 360)]
    [SerializeField] private float _rotationAmount;
    [SerializeField] private float _startRadius;
    [SerializeField] int _minParticleAmount;
    [SerializeField] int _maxParticleAmount;
    [SerializeField] private float _size;

    [Header("Physics")]
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    //[SerializeField] private float _drag;

    [Header("Other Settings")]
    [SerializeField] private float _minLifeTime;
    [SerializeField] private float _maxLlifeTime;

    [Header("Over Lifetime")]
    [SerializeField] private AnimationCurve _sizeOverLifetime;
    [SerializeField] private AnimationCurve _speedOverLifetime;
    [SerializeField] private AnimationCurve _rotationOverLifetime;

    private List<GameObject> _particleList = new List<GameObject>();

    private List<float> _speeds = new List<float>();
    private List<float> _rotations = new List<float>();
    private List<float> _lifeTimes = new List<float>();

    private List<Rigidbody2D> _rbs = new List<Rigidbody2D>();

    private float _time;

    // Start is called before the first frame update
    void Start()
    {
        SpawnParticles();
    }

    private void SpawnParticles()
    {
        _particleList.Clear();
        float particleAmount = Random.Range(_minParticleAmount, _maxParticleAmount + 1);
        for (int i = 0; i < particleAmount; i++)
        {
            GameObject particle = _particles[Random.Range(0, _particles.Length)];
            SpawnParticle(particle);
        }
    }

    private void SpawnParticle(GameObject particle)
    {
        Vector2 startPos = (Vector2)transform.position + new Vector2(Random.Range(-_startRadius, _startRadius), Random.Range(-_startRadius, _startRadius));
        Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(-_rotationAmount, _rotationAmount));
        //float rotationNormalized = rotation.z > 0 ? 1 : -1;
        float rotationNormalized = rotation.z / _rotationAmount;
        _rotations.Add(rotationNormalized);

        GameObject particleSpawned = Instantiate(particle, startPos, rotation);
        particleSpawned.transform.localScale = new Vector2(_size, _size);

        _particleList.Add(particleSpawned);

        Rigidbody2D rb = particleSpawned.AddComponent<Rigidbody2D>();
        _rbs.Add(rb);
        rb.gravityScale = 0;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        //rb.drag = _drag;

        float speed = Random.Range(_minSpeed, _maxSpeed);
        _speeds.Add(speed);
        Quaternion rotationDirection = Quaternion.Euler(0, 0, Random.Range(-(_angle / 2), (_angle / 2)) + transform.localRotation.eulerAngles.z);
        Vector2 direction = rotationDirection * Vector2.up;
        rb.velocity = direction.normalized * speed;

        DestroyAfterSeconds destroyAfterSeconds = particleSpawned.AddComponent<DestroyAfterSeconds>();
        destroyAfterSeconds.Time = Random.Range(_minLifeTime, _maxLlifeTime);
        _lifeTimes.Add(destroyAfterSeconds.Time);
        DeltaTimeTimer deltaTimeTimer = particleSpawned.AddComponent<DeltaTimeTimer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time += Time.deltaTime;

        if (_particleList.Count <= 0)
            return;

        for (int i = 0; i < _particleList.Count; i++)
        {
            if (_particleList[i] == null)
            {
                _particleList.Remove(_particleList[i]);
                _lifeTimes.Remove(_lifeTimes[i]);
                _rbs.Remove(_rbs[i]);
                _speeds.Remove(_speeds[i]);
                _rotations.Remove(_rotations[i]);
                continue;
            }
            float sizeMultiplier = _sizeOverLifetime.Evaluate(_time / _lifeTimes[i]);
            float speedMultiplier = _speedOverLifetime.Evaluate(_time / _lifeTimes[i]);
            float rotationMultiplier = _rotationOverLifetime.Evaluate(_time / _lifeTimes[i]);
            GameObject particle = _particleList[i];
            particle.transform.localScale = new Vector2(_size * sizeMultiplier, _size * sizeMultiplier);
            Rigidbody2D rb = _rbs[i];
            rb.velocity = rb.velocity.normalized * _speeds[i] * speedMultiplier;
            rb.MoveRotation(particle.transform.localRotation.eulerAngles.z + (rotationMultiplier * _rotations[i]));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        //transform.localRotation = Quaternion.Euler(0, 0, _rotation);
        Gizmos.DrawLine(transform.position, transform.position + (transform.up.normalized * _maxSpeed));
        Quaternion minRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z - (_angle / 2));
        Vector2 minDirection = minRotation * Vector2.up;
        Quaternion maxRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + (_angle / 2));
        Vector2 maxDirection = maxRotation * Vector2.up;
        Vector2 position = transform.position;
        Gizmos.DrawLine(transform.position + (transform.right.normalized * _startRadius), position + (minDirection * _maxSpeed));
        Gizmos.DrawLine(transform.position - (transform.right.normalized * _startRadius), position + (maxDirection * _maxSpeed));
        Gizmos.DrawWireCube(transform.position, new Vector2(_startRadius * 2, _startRadius * 2));
    }
}
