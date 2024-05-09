using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
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
