using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Data;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootTransform;

    [SerializeField] private float _waitTime;
    [SerializeField] private float _arcSize;
    [SerializeField] private float _bulletAmount;
    [SerializeField] private float _viewRange;

    [SerializeField] private LayerMask _playerMask;

    [SerializeField] private DataGameObject _player;

    private Timer _timer;

    [SerializeField] private UnityEvent _onShoot;

    private void Start()
    {
        _timer = GetComponent<Timer>();
        _timer.StartTimer(_waitTime, Shoot);
    }

    private void Shoot()
    {
        _timer.StartTimer(_waitTime, Shoot);
        if (!PlayerInSights())
        {
            return;
        }

        _onShoot?.Invoke();
        float rotationAmount = _arcSize / _bulletAmount;
        float startRotation = transform.localRotation.eulerAngles.z - (_arcSize / 2);
        for (int i = 0; i < _bulletAmount; i++)
        {
            Quaternion bulletRotation = Quaternion.Euler(0, 0, startRotation + (i * rotationAmount));
            Instantiate(_bullet, _shootTransform.position, bulletRotation);
        }
    }

    private bool PlayerInSights()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, _viewRange, _playerMask);
        return hit.Length > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRange);
    }
}
