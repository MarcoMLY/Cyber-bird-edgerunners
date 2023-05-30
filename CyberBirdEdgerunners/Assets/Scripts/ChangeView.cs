using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    [SerializeField] private UnityEventVariables _onChangeView;
    [SerializeField] private UnityEventVariables _onRevealEnemies;
    [SerializeField] private UnityEventVariables _onCanChangeView;
    private bool _isEnemyVisible;

    [SerializeField] private float _changeViewTimer;
    [SerializeField] private float _maxViewTime;
    [SerializeField] private float _minViewTime;
    private float _minViewTimer;

    private Timer _timer;
    private float _useTimer;

    private ChangeEnemySpeed _changeEnemySpeed;
    private ChangeTimeSpeed _changeTimeSpeed;

    private void Start()
    {
        _onCanChangeView?.Invoke(this, 0);
        _timer = GetComponent<Timer>();
        _changeEnemySpeed = GetComponent<ChangeEnemySpeed>();
        _changeTimeSpeed = GetComponent<ChangeTimeSpeed>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageInputs();
    }

    void ManageInputs()
    {
        if (_minViewTimer > 0)
        {
            _minViewTimer -= Time.unscaledDeltaTime;
            return;
        }

        if (!Input.GetKey(KeyCode.Space) && _timer.FindTimer(ChangeState))
        {
            _timer.RemoveTimer(ChangeState);
            ChangeState();
        }

        if (_useTimer > 0)
        {
            DecreaseTimerFunction(Time.unscaledDeltaTime);
            return;
        }

        if (Input.GetMouseButton(0))
        {
            return;
        }

        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }

        ChangeState();
        _timer.StartTimer(_maxViewTime, ChangeState);
        _minViewTimer = _minViewTime;
    }

    private void DecreaseTimerFunction(float amount)
    {
        _useTimer -= amount;
        if (_useTimer <= 0)
        {
            _onCanChangeView?.Invoke(this, 0);
        }
    }

    public void DecreaseTimer(Component component, int variable)
    {
        float decreaseAmount = 1;
        DecreaseTimerFunction(decreaseAmount);
    }

    private void ChangeState()
    {
        _useTimer = _changeViewTimer;
        if (_isEnemyVisible)
        {
            _isEnemyVisible = false;
            _onChangeView?.Invoke(this, 0);

            _changeEnemySpeed.ResetSpeed();
            _changeTimeSpeed.ResetSpeed();
            return;
        }

        _onRevealEnemies?.Invoke(this, 0);
        _isEnemyVisible = true;
        _onChangeView?.Invoke(this, 1);

        float newEnemySpeed = 0.2f;
        float newTimeSpeed = 0.3f;
        _changeEnemySpeed.ChangeSpeed(newEnemySpeed);
        _changeTimeSpeed.ChangeSpeed(newTimeSpeed);
    }
}
