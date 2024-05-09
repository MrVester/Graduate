using System.Collections;
using TDController;
using UnityEngine;
using System;
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    public Transform spotToGrow;
    public float speed = 2f;
    public float attackDamage=2f;
    private int facingVector = 1;

    public float leftAngle;
    public float rightAngle;
    public float rotationDuration;

   
    private bool facingRight = true;

    private bool isRotating = false;

    protected Animator anim;
    protected GameObject player;
    protected Rigidbody2D _rb;
    public float attackCoolDown=5f;
    public float timeBetweenAttacks=1f;
    public bool isStuck { get; private set; }
    protected bool isGameStopped = false;
    private Vector2 bufferVelocity;

    protected void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerController>().gameObject;
        _rb=GetComponent<Rigidbody2D>();
       
    }
    private void Start()
    {
        /* GameEvents.current.onGameStop += DisableAnimator;
         GameEvents.current.onGameStart += EnableAnimator;*/
        GameEvents.current.onGameStop += GameStopped;
        GameEvents.current.onGameStart += GameStarted;
    }
    private void OnDestroy()
    {

       /* GameEvents.current.onGameStop -= DisableAnimator;
        GameEvents.current.onGameStart -= EnableAnimator;*/
        GameEvents.current.onGameStop -= GameStopped;
        GameEvents.current.onGameStart -= GameStarted;
    }

    private void GameStarted()
    {
        ReturnControl();
        isGameStopped = false;
        print("GameStarted");
        anim.enabled = true;
        _rb.isKinematic = false;

    }
    private void GameStopped()
    {
        TakeAwayControl();
        _rb.isKinematic = true;
        isGameStopped = true;
        print("GameStopped");
        anim.enabled = false;
    }
    protected virtual void TakeAwayControl()
    {
        bufferVelocity = _rb.velocity;
        _rb.velocity = Vector2.zero;
    }

    protected virtual void ReturnControl()
    {
        _rb.velocity = bufferVelocity;
    }
    private void EnableAnimator()
    {
       anim.enabled = true;
    }
    private void DisableAnimator()
    {

        anim.enabled = false;
    }
    public void Stuck(float time)
    {
        isStuck = true;
        anim.SetBool("IsStuck", true);
        StartCoroutine(StuckCor(time));
    }
    public void UnStuck()
    {
        anim.SetBool("IsStuck", false);
        isStuck=false;
    }

    IEnumerator StuckCor(float time)
    {
        yield return new WaitForSeconds(time);
        UnStuck();
        yield return null;
    }
    public void StartDeath()
    {
        TurnOffCollider();
        anim.SetTrigger("Dead");
    }
    private void TurnOffCollider()
    {
        _rb.isKinematic = true;
        GetComponent<CircleCollider2D>().isTrigger = true;
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

    public Animator GetAnimator()
    {
        return anim;
    }
    public GameObject GetPlayer()
    {
        return player;
    }

}
