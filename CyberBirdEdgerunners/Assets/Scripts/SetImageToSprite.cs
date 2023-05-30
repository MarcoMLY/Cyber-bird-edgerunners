using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetImageToSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Image _image;

    // Start is called before the first frame update
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        _image.sprite = _spriteRenderer.sprite;
    }
}
