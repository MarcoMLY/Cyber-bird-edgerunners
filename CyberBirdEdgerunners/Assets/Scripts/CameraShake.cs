using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void SmallCameraShake(Component component, int variable)
    {
        _anim.Play("SmallCameraShake");
    }

    public void MediumCameraShake(Component component, int variable)
    {
        _anim.Play("MediumCameraShake");
    }

    public void BigCameraShake(Component component, int variable)
    {
        _anim.Play("BigCameraShake");
    }
}
