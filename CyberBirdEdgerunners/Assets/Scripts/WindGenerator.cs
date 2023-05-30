using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] _winds;
    [SerializeField] private float _spawnAreaSize;
    [SerializeField] private int _windChanceParSpawn;
    [SerializeField] private float _timeBetweenSpawn;
    private float _timer;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        if (Random.Range(0.000f, 100.000f) > _windChanceParSpawn)
        {
            return;
        }
        int randomWind = Random.Range(0, _winds.Length);
        float randomX = Random.Range(-_spawnAreaSize / 2, _spawnAreaSize / 2);
        float randomY = Random.Range(-_spawnAreaSize / 2, _spawnAreaSize / 2);
        Vector2 randomPos = new Vector2(randomX, randomY);
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        Instantiate(_winds[randomWind], randomPos, randomRotation, transform);
        _timer = _timeBetweenSpawn;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, new Vector2(_spawnAreaSize, _spawnAreaSize));
    }
}
