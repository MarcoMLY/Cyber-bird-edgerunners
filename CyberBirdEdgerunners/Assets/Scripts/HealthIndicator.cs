using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Data;

public class HealthIndicator : MonoBehaviour
{
    [SerializeField] private DataGameObject _playerData;
    private PlayerHealth _playerHealth;
    private int _health;

    private Image _image;

    // Start is called before the first frame update
    private void Start()
    {
        _playerHealth = _playerData.Variable.GetComponent<PlayerHealth>();
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _health = _playerHealth.HealthProperty;
        _image.fillAmount = (float)_health / (float)_playerHealth.MaxHealth;
    }
}
