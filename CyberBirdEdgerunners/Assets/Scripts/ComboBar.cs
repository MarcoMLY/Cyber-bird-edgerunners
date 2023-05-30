using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class ComboBar : MonoBehaviour
{
    [SerializeField] private Text _comboText;
    [SerializeField] private ComboSystem _comboSystem;
    private Slider _slider;

    [SerializeField] private GameObject[] _graphics;

    [SerializeField] private UnityEventIntiger _onComboint;
    [SerializeField] private UnityEvent _onCombo;
    [SerializeField] private UnityEvent _onEnableGraphics;
    private bool _grahpicsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.maxValue = _comboSystem.ComboTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _slider.value = _comboSystem.ComboTimer;

        if (_comboSystem.ComboTimer <= 0 || _comboSystem.Combo <= 1)
        {
            DisableGraphics();
            _grahpicsOn = false;
            return;
        }
        if (!_grahpicsOn)
        {
            EnableGraphics();
            _grahpicsOn = true;
        }
    }

    public void ComboText(int combo)
    {
        _comboText.text = "+" + combo;
    }

    public void OnCombo(Component component, int combo)
    {
        _onComboint?.Invoke(combo);
        _onCombo?.Invoke();
    }

    private void DisableGraphics()
    {
        foreach (GameObject graphic in _graphics)
        {
            graphic.SetActive(false);
        }
    }

    private void EnableGraphics()
    {
        foreach (GameObject graphic in _graphics)
        {
            graphic.SetActive(true);
        }
        _onEnableGraphics?.Invoke();
    }
}
