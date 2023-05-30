using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScore : MonoBehaviour
{
    [SerializeField] private GameObject _scoreObject;
    [SerializeField] private int amount;

    public void SpawnScoreVisual()
    {
        Vector3 randomPosition = new Vector3(transform.position.x + Random.Range(-0.4f, 0.4f), transform.position.y + Random.Range(-0.4f, 0.4f), -2);
        ScoreVisual scoreVisual = Instantiate(_scoreObject, randomPosition, Quaternion.identity).GetComponent<ScoreVisual>();
        scoreVisual.amount = amount;
    }

    public void SpawnScoreVisual(int amount)
    {
        Vector3 randomPosition = new Vector3(transform.position.x + Random.Range(-0.4f, 0.4f), transform.position.y + Random.Range(-0.4f, 0.4f), -2);
        ScoreVisual scoreVisual = Instantiate(_scoreObject, randomPosition, Quaternion.identity).GetComponent<ScoreVisual>();
        scoreVisual.amount = amount;
    }
}
