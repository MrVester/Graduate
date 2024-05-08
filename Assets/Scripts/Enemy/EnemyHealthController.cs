using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : HealthController
{

    [SerializeField] private Material enemyMat;
    [SerializeField] private Color flashEnemyColor;
    [SerializeField] private GameObject deathParticles;
    private Color defaultColor;
    private EnemyController enemyController;
    public float secondsFlash;
    public float secondsToDestroy = 1f;
    public event Action onDeath;
    //protected DamageFlash _damageFlash;
    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        enemyMat.color=Color.white;
        defaultColor= enemyMat.color;
    }
    protected new void Start()
    {
        base.Start();
        //enemyAnimator = GetComponent<Animator>();
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
            //StartCoroutine(FlashCoroutine());
            // _damageFlash.Flash(Color.white);
        }
        else
        {
            health = 0;
        }
        // if hp is less than 0, call EnemyDied event
        if (health <= 0 && !isDead)
        {
            StartCoroutine(FlashCoroutine());
            enemyController.StartDeath();
            isDead = true;
            EnemyDied();
        }
        else
        if (health > 0 && !isDead)
        {
            StartCoroutine(FlashCoroutine());
            Debug.Log("Enemy health: " + health);
        }

    }
    IEnumerator FlashCoroutine()
    {
       // print("FlashCoroutine");
        yield return ChangeColorCoroutine(defaultColor, flashEnemyColor, secondsFlash / 2);
        yield return ChangeColorCoroutine(flashEnemyColor, defaultColor, secondsFlash / 2);
       /* while (elapsedTime < secondsFlash/2)
        {   
            enemyMat.color=Color.Lerp(defaultColor, flashEnemyColor, (elapsedTime / (secondsFlash / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        elapsedTime = 0;
        while (elapsedTime  < secondsFlash/2 )
        {
            print("SecondWhile");
            enemyMat.color=Color.Lerp(flashEnemyColor, defaultColor, (elapsedTime / (secondsFlash / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
*/
        // Make sure we got there
        enemyMat.color = defaultColor;
        
        yield return null;

    }
    private IEnumerator ChangeColorCoroutine(Color fromColor,Color toColor,float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < secondsFlash / 2)
        {
           // print("SecondWhile");
            enemyMat.color = Color.Lerp(fromColor, toColor, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
    
    public override void Kill()
    {
        base.Kill();
        EnemyDied();
    }
    // Update is called once per frame
    private void EnemyDied()
    {

        //enemyAnimator.SetTrigger("Dead");
        StartCoroutine(DestroyEnemy(secondsToDestroy<secondsFlash?secondsFlash:secondsToDestroy));

    }

    IEnumerator DestroyEnemy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(deathParticles,transform.position,Quaternion.identity);
        onDeath?.Invoke();
        Destroy(gameObject);
        //EnemiesCounter.current.DecrementEnemiesAmount();
        yield return null;
    }
}
