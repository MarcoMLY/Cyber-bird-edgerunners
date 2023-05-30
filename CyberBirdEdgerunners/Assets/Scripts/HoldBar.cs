using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class HoldBar : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    private Slider _slider;

    [SerializeField] private GameObject _sliderGraphics;

    private bool _holdDashing;

    [SerializeField] private UnityEvent _onHoldBarFull;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _attack.MaxDashSpeed - _attack.MinDashSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (_slider.value >= _slider.maxValue)
        {
            _onHoldBarFull?.Invoke();
            _slider.value = _slider.maxValue;
        }

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0) && _attack.StateProperty == Attack.State.Hold && !_holdDashing)
        {
            _sliderGraphics.SetActive(true);
            _holdDashing = true;
        }

        if (_holdDashing)
        {
            _slider.value += Time.unscaledDeltaTime * _attack.HoldTimeStrengthMultiplier;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _sliderGraphics.SetActive(false);
            _holdDashing = false;
            _slider.value = 0;
        }

        if (_attack.StateProperty != Attack.State.Hold)
        {
            _holdDashing = false;
            _slider.value = 0;
            _sliderGraphics.SetActive(false);
        }
    }
}
