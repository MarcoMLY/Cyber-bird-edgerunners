using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class CursorMove : MonoBehaviour
{
    [SerializeField] private DataGameObject _cameraData;
    private Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = _cameraData.Variable.GetComponent<Camera>();
        Cursor.visible = false;
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;
    }
}
