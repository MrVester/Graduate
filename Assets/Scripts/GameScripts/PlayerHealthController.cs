using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDController;
public class PlayerHealthController : HealthController
{

    private PlayerController playerController;
    public HealthBar healthBar;
    //private DamageFlash _damageFlash;

    private new void Start()
    {

        base.Start();
        playerController = GetComponentInChildren<PlayerController>();
        healthBar.SetMaxHealth(maxHealth);
       // _damageFlash = GetComponent<DamageFlash>();

        /// CharacterEvents.current.onTakeDamage += TakeDamage;
    }

    public override void TakeDamage(float damage)
    {
        //BloodParticles.Play();

        // TODO: Add knockback
        health -= damage;
        // if hp is less than 0, call PlayerDied event


        if (!isDead)
        {
           // AudioController.current.PlayHitSound();
            //_damageFlash.Flash(Color.white);

        }
        else
        {
            health = 0;
        }
        if (health <= 0 && !isDead)
        {
            health = 0;
            healthBar.SetHealth(0);
            isDead = true;
            KillPlayer();
        }

        else

        if (health > 0 && !isDead)
        {

            healthBar.SetHealth(health);
            Debug.Log("Player health: " + health);
        }
    }

    public override void Kill()
    {
        base.Kill();
        healthBar.SetHealth(0);
        KillPlayer();
    }
    private void KillPlayer()
    {
        GameEvents.current.Death();
        GameEvents.current.GameStop();
        
        //playerController.PlayDeathAnimation();

        //OLD playerController.animator.SetBool("Dead", true);
    }

}