using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        IDamagable damagable;
        if (col.TryGetComponent<IDamagable>(out damagable))
        {
            damagable.Kill();
        }
    }
}
