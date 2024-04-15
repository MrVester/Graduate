using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseFSM : StateMachineBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public PlayerHealthController playerHealth;
    public Rigidbody2D rb;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        player = enemy.GetComponentInParent<EnemyController>().GetPlayer();
        playerHealth = player.GetComponent<PlayerHealthController>();
        rb = enemy.GetComponentInParent<Rigidbody2D>();
    }
}
