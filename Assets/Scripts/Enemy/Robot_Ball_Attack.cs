using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Robot_Ball_Attack : EnemyBaseFSM
{
    private float speed;
    private float attackTimer;
    private int currentFacingVector;
    Robot_BallController enemyController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyController = enemy.GetComponentInParent<Robot_BallController>();
        speed = enemyController.speed;
        attackTimer=0f;
        enemyController.LookAtPlayer();
        currentFacingVector = enemyController.GetFacingVector();
        enemyController.StartAttacking();
        AudioController.current.PlayRobotBallRollingSound();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController.LookAtPlayer();
        rb.velocity = new Vector2(speed * currentFacingVector, rb.velocity.y);
        attackTimer += Time.deltaTime;
        animator.SetFloat("AttackTimer", attackTimer);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackTimer = 0f;
        animator.SetFloat("AttackTimer", 0);
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.inertia = 0;
        enemyController.StartAttackCoolDown();
        enemyController.EndAttacking();
        AudioController.current.StopRobotBallRollingSound();
    }
}
