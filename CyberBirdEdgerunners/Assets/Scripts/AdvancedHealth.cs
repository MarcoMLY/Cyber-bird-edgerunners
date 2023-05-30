using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class AdvancedHealth : Health
{
    private Rigidbody2D _rb;
    private Timer _timer;
    protected bool _canBeDamaged = true;

    [SerializeField] private UnityEventVariables _onDamaged;
    [SerializeField] private UnityEvent _onKnockbackFinished;
    [SerializeField] private UnityEventVariables _onDie;
    [SerializeField] private UnityEvent _onImmune;
    [SerializeField] private UnityEvent _onDeImmune;

    [SerializeField] private float _knockbackForce;
    [SerializeField] private float _knockbackTime;
    [SerializeField] private float _immuneTime;

    [SerializeField] private GameObject[] _dieEffects;

    [SerializeField] private GameObject _hitParticles;
    [SerializeField] private GameObject _DieParticles;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _timer = GetComponent<Timer>();
    }

    public override void Damage(GameObject attacker, int damage)
    {
        if (!_canBeDamaged)
            return;
        base.Damage(attacker, damage);
        _rb.velocity = (transform.position - attacker.transform.position).normalized * _knockbackForce;
        _timer.StartTimer(_knockbackTime, KnockbackFinished);
        Immune();
        _timer.StartTimer(_immuneTime, DeImmune);
        if (HealthProperty <= 0)
        {
            _onDie?.Invoke(attacker.transform, damage);
            return;
        }
        _onDamaged?.Invoke(attacker.transform, damage);
    }

    public void HurtParticles(Component component, int variable)
    {
        transform.up = component.transform.up;
        Instantiate(_hitParticles, transform.position, transform.rotation);
    }

    public void DieParticles(Component component, int variable)
    {
        transform.up = component.transform.up;
        Instantiate(_DieParticles, transform.position, transform.rotation);
    }

    private void KnockbackFinished()
    {
        _onKnockbackFinished?.Invoke();
    }

    public void Immune()
    {
        _canBeDamaged = false;
        _onImmune?.Invoke();
    }

    public void DeImmune()
    {
        _canBeDamaged = true;
        _onDeImmune?.Invoke();
    }

    public void DieEffect(Component component, int variable)
    {
        int randomDieEffect = Random.Range(0, _dieEffects.Length);
        GameObject dieEffect = Instantiate(_dieEffects[randomDieEffect], transform.position, transform.rotation);
        dieEffect.transform.localScale = transform.localScale;
    }
}
