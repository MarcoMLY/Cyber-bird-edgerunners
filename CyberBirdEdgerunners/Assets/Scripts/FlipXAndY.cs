using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipXAndY : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.flipX = Random.Range(0, 2) == 0;
        _spriteRenderer.flipY = Random.Range(0, 2) == 0;
    }
}
