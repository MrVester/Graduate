using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TDController;
public class PlayerHealthController : HealthController, IDamagable
{

    [SerializeField] private HealthBar healthBar;
    [SerializeField]private float greenHeartHealRatio;
    [SerializeField]private float timeBetweenHeal=1;
    private bool canGreenHeartHeal=false;
    private PlayerController playerController;
    private AnimationController playerAnimator;
    private bool greenHeartHealPaused;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerAnimator = GetComponentInChildren<AnimationController>();
    }
    private new void Start()
    {

        base.Start();
       
        healthBar.SetMaxHealth(maxHealth);

        GameEvents.current.onGameStart += UnPauseGreenHeartHeal;
        GameEvents.current.onGameStop += PauseGreenHeartHeal;
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        if (!isDead)
        {

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
 
    private void PauseGreenHeartHeal()
    {
        greenHeartHealPaused=true;
    }
    private void UnPauseGreenHeartHeal()
    {
        greenHeartHealPaused = false;
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
    private IEnumerator GreenHeartHeal()
    {
        yield return new WaitForSeconds(timeBetweenHeal);
        while (canGreenHeartHeal)
        {
            if (!greenHeartHealPaused)
            {
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
                yield return new WaitForSeconds(timeBetweenHeal);
            }
            yield return null;
        }
    }
}