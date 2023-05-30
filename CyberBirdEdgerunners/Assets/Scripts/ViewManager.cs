using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    private RawImage _image;
    [SerializeField] private LayerMask _normalLayer;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Color _normalBackroundColor;
    [SerializeField] private Color _enemyBackroundColor;
    [SerializeField] private Camera _camera;

    [SerializeField] private UnityEventVariables _onHideEnemies;
    [SerializeField] private UnityEvent _onRevealEnemies;


    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<RawImage>();
    }

    public void HideEnemies()
    {
        _camera.cullingMask = _normalLayer;
    }

    public void RevealEnemies()
    {
        _camera.cullingMask = _enemyLayer;
    }

    public void ChangeView(Component component, int value)
    {
        switch (value)
        {
            case 0:
                HideEnemies();
                _onHideEnemies?.Invoke(this, 0);
                break;
            case 1:
                RevealEnemies();
                _onRevealEnemies?.Invoke();
                break;
            default:
                break;
        }
    }

    public void NormalColor()
    {
        _camera.backgroundColor = _normalBackroundColor;
    }

    public void EnemyColorColor()
    {
        _camera.backgroundColor = _enemyBackroundColor;
    }

    public void ChangeColor(Component component, int value)
    {
        switch (value)
        {
            case 0:
                NormalColor();
                break;
            case 1:
                EnemyColorColor();
                break;
            default:
                break;
        }
    }
}
