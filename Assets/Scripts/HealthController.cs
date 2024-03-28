using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthController : MonoBehaviour
{
    public float maxHealth = 10.0f;
    [SerializeField]
    protected float health;
    protected bool isDead = false;
    // Start is called before the first frame update
    protected void Start()
    {
        health = maxHealth;
    }

    public abstract void TakeDamage(float damage);
    public virtual void Kill()
    {
        health = 0;
        isDead = true;
    }
}
