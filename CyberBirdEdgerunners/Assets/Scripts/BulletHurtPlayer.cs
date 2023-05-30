using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BulletHurtPlayer : HurtPlayer
{
    [SerializeField] private UnityEvent _onHitPlayer;
    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
        _onHitPlayer?.Invoke();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
