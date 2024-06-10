using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Robot_BallController : EnemyController
{
    
    [SerializeField] private float radiusToAttack=20f;
    [SerializeField] private float yDistanceToAttack=5f;
    private float distance;
    private float yDistance;
    private bool canAttack_Anim = false;
    private bool canAttack_Cor = true;
    private new void Awake()
    {   
        base.Awake();

    }
   
    public void StartAttackCoolDown()
    {

        StartCoroutine(AttackCD());
    }
    public void StartAttacking()
    {
        //print("StartAttacking");
        canAttack_Anim = true;
    }
    public void EndAttacking()
    {
        //print("EndAttacking");
        canAttack_Anim = false;
    }
    private IEnumerator AttackCD()
    {
        anim.SetBool("OnAttackCoolDown", true);

        yield return new WaitForSeconds(attackCoolDown);

        anim.SetBool("OnAttackCoolDown", false);
        yield return null;
    }
    public void ResetAttackCoolDown()
    {
        attackCoolDown = 0f;
        anim.SetBool("OnAttackCoolDown", false);
    }
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= radiusToAttack)
        {
            anim.SetBool("IsInRadius", true);
        }
        else
        {
            anim.SetBool("IsInRadius", false);
        }
        yDistance = Mathf.Abs(transform.position.y - player.transform.position.y);
        if (yDistance<=yDistanceToAttack)
        {
            anim.SetBool("IsYDistance", true);
        }
        else
        {
            anim.SetBool("IsYDistance", false);
        }
    }
   

    private void OnTriggerStay2D(Collider2D collision)
    {
        HealthController healthContr;
        if (collision.gameObject.TryGetComponent(out healthContr))
        {
            if (canAttack_Anim)
            {
                if (canAttack_Cor&&!isGameStopped)
                {
                    healthContr.gameObject.GetComponent<HealthController>().TakeDamage(attackDamage);
                    AudioController.current.PlayRobotBallHitSound();
                    StartCoroutine(TimeBetweenAttacks());
                }

            }

        }

    }

    private IEnumerator TimeBetweenAttacks()
    {
        canAttack_Cor = false;
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack_Cor = true;
        yield return null;

    }
}
