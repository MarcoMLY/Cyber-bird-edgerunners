using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayTutorial : MonoBehaviour
{
    private Animator _anim;

    [SerializeField] private UnityEvent _onStart;
    [SerializeField] private UnityEvent _onPressSpace;
    [SerializeField] private UnityEvent _onClickMouse;

    // Start is called before the first frame update
    private void Start()
    {
        _anim = GetComponent<Animator>();
        _onStart.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _onPressSpace?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            _onClickMouse?.Invoke();
        }
    }

    public void StartTutorial()
    {
        _anim.SetBool("OnTutorial", true);
    }

    public void EndTutorial()
    {
        _anim.SetBool("OnTutorial", false);
        float disableObjectTime = 2;
        Invoke("DisableObject", disableObjectTime);
    }

    private void DisableObject()
    {
        gameObject.SetActive(false);
    }

    public void ChangeTutorial()
    {
        if (!_anim.GetBool("OnTutorial"))
        {
            StartTutorial();
            return;
        }
        _anim.SetBool("OnTutorial", false);
    }
}
