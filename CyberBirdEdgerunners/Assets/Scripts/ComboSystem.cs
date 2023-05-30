using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using Data;

public class ComboSystem : MonoBehaviour
{
    [SerializeField] private DataInt _score;
    [SerializeField] private int _maxCombo;
    [SerializeField] private int _maxComboTwo;
    [SerializeField] private int _comboAddon;
    [SerializeField] private float _comboTime;
    private float _comboTimer;
    private int _combo = 1;

    public float ComboTime { get => _comboTime; }
    //public float ComboAddon { get => _comboAddon; }
    public float ComboTimer { get => _comboTimer; }
    public float Combo { get => _combo; }

    private SpawnScore _spawnScore;

    [SerializeField] private GameObject _confetti;

    [SerializeField] private UnityEventVariables _onCombo;
    [SerializeField] private UnityEvent _onSuperCombo;
    [SerializeField] private UnityEvent _onSuperDuperCombo;

    private void Start()
    {
        _spawnScore = GetComponent<SpawnScore>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_comboTimer > 0)
        {
            _comboTimer -= Time.deltaTime;
        }
    }

    public void SpawnParticles()
    {
        GameObject particles = Instantiate(_confetti, transform.position, transform.rotation);
        particles.transform.up = transform.parent.up;
    }

    public void AddScore(Component component, int variable)
    {
        CheckCombo();
        if (_combo > _maxComboTwo)
        {
            _combo = 1;
        }
        _score.Add(_combo);
        _spawnScore.SpawnScoreVisual(_combo);
        _comboTimer = _comboTime;
        if (_combo == _maxCombo)
        {
            _onSuperCombo?.Invoke();
        }
        if (_combo == _maxComboTwo)
        {
            _onSuperDuperCombo?.Invoke();
            _combo = 1;
        }
    }

    private void CheckCombo()
    {
        if (_comboTimer > 0)
        {
            if (_combo == 1)
            {
                _combo += 1;
                _onCombo?.Invoke(this, _combo);
                return;
            }
            _combo += _comboAddon;
            _onCombo?.Invoke(this, _combo);
            return;
        }
        _combo = 1;
    }
}
