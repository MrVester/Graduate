using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainCollider : MonoBehaviour
{
    private PlayerHealthController healthController;
    private PlayerSkills playerSkills;
    private bool canRainDamage=false;
    private bool rainDamagePaused=false;
    [SerializeField] private ParticleSystem rainParticles;
    [SerializeField] private float timeBetweenDamage=1;
    [SerializeField] private float rainDamage=0.7f;
    private void Start()
    {
        GameEvents.current.onGameStart += UnPauseRainDamage;
        GameEvents.current.onGameStop += PauseRainDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(!rainParticles.isPlaying)
            rainParticles.Play();
            if(healthController == null|| playerSkills == null)
            {
                healthController = collision.GetComponent<PlayerHealthController>();
                playerSkills = collision.GetComponent<PlayerSkills>();
            }
                
            if (playerSkills.GetSkill(SkillColor.Green) == Skills.GreenSkill2)
            {
                StopRainDamage();
            }
            else
            {
                StartRainDamage();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (rainParticles.isPlaying)
                rainParticles.Stop();
            StopRainDamage();
        }
    }
    private void PauseRainDamage()
    {
        rainDamagePaused = true;
    }
    private void UnPauseRainDamage()
    {
        rainDamagePaused = false;
    }
    private void StopRainDamage()
    {
        canRainDamage = false;
        StopCoroutine(PlayerRainDamage());
    }
    private void StartRainDamage()
    {
        canRainDamage = true;
        StartCoroutine(PlayerRainDamage());
    }
    private IEnumerator PlayerRainDamage()
    {
        yield return new WaitForSeconds(timeBetweenDamage);
        while (canRainDamage)
        {
            if (!rainDamagePaused)
            {
                healthController.TakeDamage(rainDamage);
                yield return new WaitForSeconds(timeBetweenDamage);
            }
            yield return null;
        }
    }
}
