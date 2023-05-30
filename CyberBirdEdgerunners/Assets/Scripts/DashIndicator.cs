using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DashIndicator : MonoBehaviour
{
    [SerializeField] private Attack _attack;
    [SerializeField] private UnityEvent _onCanDash;

    [SerializeField] private ChangeColor _whiteSprite;
    [SerializeField] private ChangeImageColor _whiteImage;
    [SerializeField] private ChangeDefultColor _defultSprite;
    [SerializeField] private ChangeDefultImageColor _defultImage;
    [SerializeField] private Timer _timer;
    [SerializeField] private float _flickerTime;

    [SerializeField] private GameObject _particles;

    private void Start()
    {
        _attack.OnCanDash += OnCanDash;
    }

    private void OnDisable()
    {
        _attack.OnCanDash -= OnCanDash;
    }

    public void OnCanDash()
    {
        _onCanDash?.Invoke();
    }

    public void Flicker()
    {
        _whiteSprite.ChangeSpriteColor();
        _whiteImage.ChangeSpriteColor();
        _timer.StartTimer(_flickerTime, _defultSprite.SetDefult);
        _timer.StartTimer(_flickerTime, _defultImage.SetDefult);
    }

    public void SpawnParticles()
    {
        Instantiate(_particles, transform.position, transform.rotation);
    }
}
