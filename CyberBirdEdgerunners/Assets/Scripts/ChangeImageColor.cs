using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class ChangeImageColor : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _color;

    public void ChangeSpriteColor()
    {
        _image.color = _color;
    }

    public void ChangeSpriteColor(Component component, int variable)
    {
        _image.color = _color;
    }
}
