using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChangeDefultImageColor : MonoBehaviour
{
    [SerializeField] private ChangeImageColor _colorOne;
    [SerializeField] private ChangeImageColor _colorTwo;
    private Action _ChangedefultColor;

    // Start is called before the first frame update
    private void Start()
    {
        _ChangedefultColor = _colorOne.ChangeSpriteColor;
    }


    public void ChangeDefultToOne(Component component, int variable)
    {
        _ChangedefultColor = _colorOne.ChangeSpriteColor;
        SetDefult();
    }

    public void ChangeDefultToTwo(Component component, int variable)
    {
        _ChangedefultColor = _colorTwo.ChangeSpriteColor;
        SetDefult();
    }

    public void SetDefult()
    {
        _ChangedefultColor();
    }

    public void SetDefult(Component component, int variable)
    {
        _ChangedefultColor();
    }
}
