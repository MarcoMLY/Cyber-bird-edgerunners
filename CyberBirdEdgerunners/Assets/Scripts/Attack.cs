using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Data;

public class Attack : MonoBehaviour
{
    public enum State
    {
        Defult,
        Hold,
        Dashing,
        Knockback
    }

    private Rigidbody2D _rb;
    private DeltaTimeTimer _deltaTimer;
    private ChangeTimeSpeed _changeTimeSpeed;

    [Header("Events")]
    [SerializeField] public UnityEvent OnUsingForce;
    [SerializeField] public UnityEventVariables OnPushForce;
    [SerializeField] public UnityEventVariables OnDashEvent;
    [SerializeField] public UnityEvent OnStoppedUsingForce;
    [SerializeField] public UnityEventVariables OnEnemyHit;

    [Header("Dash Variables")]
    [SerializeField] private float _dashTime;
    [SerializeField] private float _maxdashSpeed;
    [SerializeField] private float _mindashSpeed;
    private float _currentDashSpeed;
    [SerializeField] private float _holdTimeStrengthMultiplier;
    [SerializeField] private float _holdSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _dashBufferTime;
    private float _dashBufferTimer;

    public float MaxDashSpeed { get => _maxdashSpeed; }
    public float MinDashSpeed { get => _mindashSpeed; }
    public float HoldTimeStrengthMultiplier { get => _holdTimeStrengthMultiplier; }

    [Header("Other Variables")]
    [SerializeField] private float _waitTime;
    [SerializeField] private float _knockBackForce;
    [SerializeField] private float _knockBackTime;
    private float _waitTimer;
    private State _state;

    public State StateProperty
    {
        get => _state;
    }

    [Header("Camera")]
    [SerializeField] private DataGameObject _cameraData;
    private Camera _camera;

    [Header("Juice")]
    [SerializeField] private float _pauseOnHitTime;
    [SerializeField] public UnityEventVariables OnDashColorEvent;

    private bool _enabled = true;
    [HideInInspector] public bool _pressonOnDisabled { get; private set; }

    [Header("Dash Hitbox")]
    [SerializeField] private Transform _hitBoxPos;
    [SerializeField] private Vector2 _hitBoxSize;
    [SerializeField] private LayerMask _enemyMask;

    [HideInInspector] public System.Action OnDash;
    [HideInInspector] public System.Action OnEndDash;
    [HideInInspector] public System.Action OnCanDash;

    [Header("Effects")]
    [SerializeField] private GameObject _hitEnemyEffect;
    [SerializeField] private GameObject _dashParticles;

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _deltaTimer = GetComponent<DeltaTimeTimer>();
        _changeTimeSpeed = GetComponent<ChangeTimeSpeed>();
        _rb = GetComponent<Rigidbody2D>();
        _camera = _cameraData.Variable.GetComponent<Camera>();
    }

    // Update is called once per frame
    private void Update()
    {
        AttackInputs();
        SetDefultState();
    }

    private void FixedUpdate()
    {
        if (_dashBufferTimer > 0)
        {
            _dashBufferTimer -= Time.deltaTime;
        }
        Collision();
    }

    private void SetDefultState()
    {
        if (_deltaTimer.FindTimer(StopUsingForce))
        {
            return;
        }

        if (_state == State.Hold && _waitTimer <= 0)
        {
            return;
        }

        if (_state == State.Dashing)
        {
            OnEndDash?.Invoke();
        }

        _state = State.Defult;
    }

    private void Collision()
    {
        if (_dashBufferTimer <= 0)
        {
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapBoxAll(_hitBoxPos.position, _hitBoxSize, transform.localRotation.eulerAngles.z, _enemyMask);

        List<Collider2D> enemyColliders = new List<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject.GetComponent<IDamageable>() == null)
            {
                return;
            }

            enemyColliders.Add(collider);
        }

        for (int i = 0; i < enemyColliders.Count; i++)
        {
            _dashBufferTimer = 0;
            OnEnemyHit?.Invoke(enemyColliders[i].transform, 0);
            int damage = 1;
            enemyColliders[i].gameObject.GetComponent<IDamageable>()?.Damage(gameObject, damage);
            _waitTimer = -0.1f;

            _deltaTimer.RemoveTimer(StopUsingForce);
            _deltaTimer.RemoveTimer(OnAttack);
            Knockback();

            _changeTimeSpeed.PauseTemporarily(_pauseOnHitTime);

            if (i == enemyColliders.Count - 1)
            {
                OnEndDash?.Invoke();
                OnPushForce?.Invoke(this, 0);
                _state = State.Knockback;
            }
        }
    }

    public void EnemyEffect(Component component, int variable)
    {
        Instantiate(_hitEnemyEffect, _hitBoxPos.position, _hitBoxPos.rotation);
    }

    public void DashParticles(Component component, int variable)
    {
        GameObject particles = Instantiate(_dashParticles, transform.position, transform.rotation);
        particles.transform.up = -transform.up;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_hitBoxPos.position, _hitBoxSize);
    }

    void Knockback()
    {
        OnUsingForce?.Invoke();
        _deltaTimer.StartTimer(_knockBackTime, StopUsingForce);
        SetVelocity(-transform.up.normalized * _knockBackForce);
    }

    private void AttackInputs()
    {
        if (_waitTimer > 0)
        {
            _waitTimer -= Time.unscaledDeltaTime;
            if (_waitTimer <= 0)
            {
                OnCanDash?.Invoke();
            }
            return;
        }

        if (Input.GetMouseButton(0) && !_enabled)
            _pressonOnDisabled = true;

        if (!_enabled)
        {
            return;
        }

        if (_state == State.Dashing)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _deltaTimer.RemoveTimer(StopUsingForce);
            float constraintTimerTime = 0.1f;
            _deltaTimer.StartTimer(constraintTimerTime, OnAttack);
            OnUsingForce?.Invoke();
            _currentDashSpeed = _mindashSpeed;
            _state = State.Hold;
        }

        if (Input.GetMouseButton(0) && _state != State.Knockback)
        {
            if (_state == State.Knockback)
            {
                _deltaTimer.RemoveTimer(StopUsingForce);
            }

            if (_state != State.Hold)
            {
                float constraintTimerTime = 0.2f;
                _deltaTimer.StartTimer(constraintTimerTime, OnAttack);
                OnUsingForce?.Invoke();
                _currentDashSpeed = _mindashSpeed;
                _state = State.Hold;
            }

            _waitTimer = -0.1f;

            if (_currentDashSpeed < _maxdashSpeed)
            {
                _currentDashSpeed += Time.unscaledDeltaTime * _holdTimeStrengthMultiplier;
            }

            float slowTimeSpeed = 0.1f;
            _changeTimeSpeed.ChangeSpeed(slowTimeSpeed);

            Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - (Vector2)transform.position;
            direction.Normalize();
            RotateTowards(direction);

            Vector2 velocity = transform.up.normalized;
            SetVelocity(velocity * _holdSpeed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _deltaTimer.RemoveTimer(OnAttack);
            OnAttack();
        }
    }

    private void OnAttack()
    {
        _pressonOnDisabled = false;
        OnDash?.Invoke();
        OnDashEvent.Invoke(this, 0);
        _state = State.Dashing;
        _waitTimer = _waitTime;
        _changeTimeSpeed.ResetSpeed();
        _deltaTimer.StartTimer(_dashTime, StopUsingForce);
        _dashBufferTimer = _dashTime + _dashBufferTime;
        Dash();
    }

    private void Dash()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePos - (Vector2)transform.position;
        direction.Normalize();
        RotateInstantly(direction);
        OnPushForce?.Invoke(this, 0);
        if (_currentDashSpeed < _mindashSpeed)
        {
            _currentDashSpeed = _mindashSpeed;
        }
        if (_currentDashSpeed > _maxdashSpeed)
        {
            _currentDashSpeed = _maxdashSpeed;
        }
        SetVelocity(direction * _currentDashSpeed);
        _currentDashSpeed = 0;
    }

    private void SetVelocity(Vector2 velocity)
    {
        _rb.velocity = velocity;
    }

    private void RotateTowards(Vector2 direction)
    {
        transform.up = Vector2.Lerp(transform.up, direction, Time.deltaTime * _rotationSpeed);
    }

    private void RotateInstantly(Vector2 direction)
    {
        transform.up = direction;
    }

    private void StopUsingForce()
    {
        OnStoppedUsingForce?.Invoke();
    }

    public void EnableAndDisable()
    {
        if (_enabled)
        {
            _enabled = false;
            return;
        }
        _enabled = true;
    }
}
