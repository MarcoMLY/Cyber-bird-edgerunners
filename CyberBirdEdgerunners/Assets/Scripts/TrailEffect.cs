using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour
{
    [SerializeField] private Gradient _gradient;
    private float _gradientProgress;

    [SerializeField] private GameObject _trailObject;
    [SerializeField] private bool _trailEffectOn = false;

    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private float _gradientProgressBetweenSpawn;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _transform;

    private Timer _timer;

    private List<GameObject> trails = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        _timer = GetComponent<Timer>();
        _gradientProgress = Random.value;
        SpawnTrail();
    }

    private void SpawnTrail()
    {
        _timer.StartTimer(_timeBetweenSpawn, SpawnTrail);
        if (!_trailEffectOn)
            return;
        GameObject trail = Instantiate(_trailObject, _transform.position, _transform.rotation);
        trails.Add(trail);
        SpriteRenderer trailSpriteRenderer = trail.GetComponent<SpriteRenderer>();
        trailSpriteRenderer.color = _gradient.Evaluate(_gradientProgress);
        trailSpriteRenderer.sprite = _spriteRenderer.sprite;
        _gradientProgress += _gradientProgressBetweenSpawn;
        if (_gradientProgress > 1)
        {
            _gradientProgress -= 1;
        }
    }

    public void EnableAndDisable()
    {
        if (_trailEffectOn)
        {
            _trailEffectOn = false;
            foreach (GameObject trail in trails)
            {
                Destroy(trail);
            }
            trails.Clear();
            return;
        }
        _trailEffectOn = true;
    }
}
