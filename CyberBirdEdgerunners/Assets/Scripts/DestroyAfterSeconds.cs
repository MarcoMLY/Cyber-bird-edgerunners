using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    [field:SerializeField] public float Time { get; set; }
    private Timer _timer;

    [SerializeField] private GameObject _destroyParticles;

    [SerializeField] private UnityEvent _onDestroy;

    // Start is called before the first frame update
    private void Start()
    {
        _timer = GetComponent<Timer>();
        _timer.StartTimer(Time, Destroy);
    }

    public void Destroy()
    {
        _onDestroy?.Invoke();
        Destroy(gameObject);
    }

    public void SpawnParticles()
    {
        Instantiate(_destroyParticles, transform.position, transform.rotation);
    }
}
