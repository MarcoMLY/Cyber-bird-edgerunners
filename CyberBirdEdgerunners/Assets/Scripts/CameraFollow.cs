using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private DataGameObject _playerData;
    private Transform _player;

    [SerializeField] private float _speed;
    [SerializeField] private float _offset;

    private void Start()
    {
        _player = _playerData.Variable.transform;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_player == null)
            return;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPosition = (Vector2)_player.position;
        Vector2 direction = mousePos - playerPosition;
        direction.Normalize();
        Vector2 position = playerPosition + (direction * _offset);
        SetPosition(position);
    }

    private void SetPosition(Vector2 position)
    {
        Vector3 playerPosition = new Vector3(position.x, position.y, -10);
        transform.position = Vector3.Lerp(transform.position, playerPosition, Time.deltaTime * _speed);
    }
}
