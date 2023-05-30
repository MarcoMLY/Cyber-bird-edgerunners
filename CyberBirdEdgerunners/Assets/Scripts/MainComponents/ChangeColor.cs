using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _color;

    public void ChangeSpriteColor()
    {
        _spriteRenderer.color = _color;
    }

    public void ChangeSpriteColor(Component component, int variable)
    {
        _spriteRenderer.color = _color;
    }
}
