using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreVisual : MonoBehaviour
{
    [SerializeField] public int amount;
    [SerializeField] public int size;
    [SerializeField] private GameObject _scoreObject;
    private TextMesh _scoreText;

    // Start is called before the first frame update
    private void Start()
    {
        _scoreText = _scoreObject.GetComponent<TextMesh>();
        _scoreText.text = "+" + amount;
        Vector2 scale = new Vector2(size + (amount * 0.5f), size + (amount * 0.5f));
        _scoreObject.transform.localScale = scale;
        Quaternion randomRotation = Quaternion.Euler(0, 0, Random.Range(-10, 10));
        _scoreObject.transform.rotation = randomRotation;
    }
}
