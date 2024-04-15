using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TDController;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    public float attackDamage=2f;
    private int facingVector = 1;
    
    public float leftAngle;
    public float rightAngle;
    public float rotationDuration;

   
    private bool facingRight = true;

    private bool isRotating = false;

    private Animator anim;
    private GameObject player;
    public float attackCoolDown=5f;
    public float timeBetweenAttacks=1f;
    private bool canAttack_Anim=false;
    private bool canAttack_Cor=true;


    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }
    private void Start()
    {

    }
    public void StartAttacking()
    {
        print("StartAttacking");
        canAttack_Anim=true;
    }
    public void EndAttacking()
    {
        print("EndAttacking");
        canAttack_Anim = false;
    }
    public void LookAtPlayer()
    {
        Vector3 characterPos = player.transform.position;
        if (characterPos.x - transform.position.x < 0 && facingRight)

        {
            RotateChar();

        }
        else

        if (characterPos.x - transform.position.x > 0 && !facingRight)

        {
            RotateChar();

        }
    }
    public int GetFacingVector()
    {
        return facingVector;
    }
    private void RotateChar()
    {
        facingVector *= -1;
        if (facingRight)
        {
            StartCoroutine(ObjectRotate(transform.rotation, Quaternion.Euler(new Vector3(0, leftAngle, 0))));
            facingRight = false;
        }
        else
           if (!facingRight)
        {
            StartCoroutine(ObjectRotate(transform.rotation, Quaternion.Euler(new Vector3(0, rightAngle, 0))));
            facingRight = true;
        }
    }
    private IEnumerator ObjectRotate(Quaternion startrot, Quaternion endrot)
    {
        isRotating = true;
        float t = 0;
        while (t < 1)
        {
            transform.rotation = Quaternion.Slerp(startrot, endrot, t);
            t += Time.deltaTime / rotationDuration;
            yield return null;
        }
        transform.rotation = endrot;
        isRotating = false;
    }

    public void StartAttackCoolDown()
    {
        
        StartCoroutine(AttackCD());
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
        //Should move AttackCD to Start later

        anim.SetFloat("Distance", Vector3.Distance(transform.position, player.transform.position));
    }
    public Animator GetAnimator()
    {
        return anim;
    }
    public GameObject GetPlayer()
    {
        return player;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        HealthController healthContr;
        if(collision.gameObject.TryGetComponent(out healthContr))
        {
            if(canAttack_Anim)
            {
                if (canAttack_Cor)
                {
                    healthContr.gameObject.GetComponent<HealthController>().TakeDamage(attackDamage);
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
