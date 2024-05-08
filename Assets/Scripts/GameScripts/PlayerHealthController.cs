using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDController;
public class PlayerHealthController : HealthController, IDamagable
{

    [SerializeField] private HealthBar healthBar;
    [SerializeField]private float greenHeartHealRatio;
    [SerializeField]private float timeBetweenHeal;
    private bool canGreenHeartHeal=false;
    private PlayerController playerController;
    private AnimationController playerAnimator;
    //private DamageFlash _damageFlash;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponentInChildren<AnimationController>();
    }
    private new void Start()
    {

        base.Start();
       
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
            healthBar.SetHealthLerp(0);
            isDead = true;
            KillPlayer();
        }

        else

        if (health > 0 && !isDead)
        {

            healthBar.SetHealthLerp(health);
            //Debug.Log("Player health: " + health);
        }
    }

    public override void Kill()
    {
        base.Kill();
        healthBar.SetHealthLerp(0);
        KillPlayer();
    }
    private void KillPlayer()
    {
        GameEvents.current.Death();
        //GameEvents.current.GameStop();
        StopGreenHeartHeal();

        playerAnimator.animator.Play("Death");
        playerAnimator.SetDead(true);
        playerAnimator.animator.SetBool("IsDead", true);
    }
    public void SetMaxHealth()
    {
        print("SetMaxHealth");
        health=maxHealth;
        healthBar.SetHealthLerp(maxHealth);
    }
    public void StartGreenHeartHeal()
    {
        canGreenHeartHeal = true;
        StartCoroutine(GreenHeartHeal());
    }
    public void StopGreenHeartHeal()
    {
        canGreenHeartHeal = false;
        StopCoroutine(GreenHeartHeal());
    }
    IEnumerator GreenHeartHeal()
    {
        while (canGreenHeartHeal)
        {
            yield return new WaitForSeconds(timeBetweenHeal);
            if (health < maxHealth)
            {
                health += greenHeartHealRatio;
                healthBar.SetHealthLerp(health);
            }

            else
            {
                health = maxHealth;
                healthBar.SetHealthLerp(health);
            }
               
            yield return null;
        }
    }
}