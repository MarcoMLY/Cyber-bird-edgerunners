using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _anim;
    private float _currentRotation;
    private float _lastRotation;

    [SerializeField] private float _rotationAmount;

    [SerializeField] private Attack _attack;

    // Start is called before the first frame update
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _attack.OnDash += Dash;
        _attack.OnEndDash += EndDash;
    }

    private void OnDisable()
    {
        _attack.OnDash -= Dash;
        _attack.OnEndDash -= EndDash;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _currentRotation = transform.parent.localRotation.eulerAngles.z;
        float rotationAmount = _currentRotation - _lastRotation;
        _lastRotation = _currentRotation;

        if (rotationAmount < _rotationAmount && rotationAmount > -_rotationAmount)
        {
            _anim.SetBool("Flapping", true);
            return;
        }

        _anim.SetBool("Flapping", false);
    }

    private void Update()
    {
        if (_attack.StateProperty != Attack.State.Hold)
        {
            _anim.SetBool("Holding", false);
            return;
        }
        if (_anim.GetBool("Holding"))
        {
            return;
        }
        _anim.SetBool("Holding", true);
        if (_attack._pressonOnDisabled)
        {
            _anim.SetTrigger("QuickHold");
            return;
        }
        _anim.SetTrigger("Hold");
    }

    private void Dash()
    {
        _anim.SetTrigger("DashIn");
        _anim.SetBool("Dashing", true);
    }

    private void EndDash()
    {
        _anim.SetBool("Dashing", false);
    }
}
