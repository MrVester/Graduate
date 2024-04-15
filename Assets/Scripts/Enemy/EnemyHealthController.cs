using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : HealthController
{


    protected Animator enemyAnimator;
    public float secondsToDestroy = 2f;
    //protected DamageFlash _damageFlash;
    protected new void Start()
    {
        base.Start();
        enemyAnimator = GetComponent<Animator>();
       // _damageFlash = GetComponent<DamageFlash>();
    }
    public override void TakeDamage(float damage)
    {
        //BloodParticles.Play();

        // TODO: Add knockback
        health -= damage;

        if (!isDead)
        {
           // AudioController.current.PlayHitSound();
           // _damageFlash.Flash(Color.white);
        }
        else
        {
            health = 0;
        }
        // if hp is less than 0, call EnemyDied event
        if (health <= 0 && !isDead)
        {
            isDead = true;
            EnemyDied();
        }
        else
        if (health > 0 && !isDead)
        {

            Debug.Log("Enemy health: " + health);
        }




    }
    public override void Kill()
    {
        base.Kill();
        EnemyDied();
    }
    // Update is called once per frame
    private void EnemyDied()
    {

        enemyAnimator.SetTrigger("Dead");
        StartCoroutine(DestroyEnemy(secondsToDestroy));

    }

    IEnumerator DestroyEnemy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
        //EnemiesCounter.current.DecrementEnemiesAmount();
        yield return null;
    }
}
