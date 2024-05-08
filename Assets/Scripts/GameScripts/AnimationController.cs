using System.Collections;
using TDController;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public Animator animator;
    private PlayerController playerController;
    public PlayerInput _input;
    public float leftAngle;
    public float rightAngle;
    public float rotationDuration;

    private bool facingRight = true;
    private int facingVector = 1;
    private bool isRotating = false;
    private bool isDead = false;
    private bool isPaused= false;
    // Start is called before the first frame update
    private void Awake()
    {
        GameEvents.current.onGameStop += PauseAnims;
        GameEvents.current.onGameStart += ResumeAnims;
        playerController= _input.gameObject.GetComponent<PlayerController>();
    }
    void Start()
    {
        playerController.GroundedChanged += SetGrounded;
        playerController.Attacked += Attack;
    }
    public void SetDead(bool isDead)
    {
        this.isDead = isDead;
    }
    public int GetFacingVector()
    {
        return facingVector;
    }
    private void PauseAnims()
    {
        animator.enabled=false;
        isPaused = true;
    }
    private void ResumeAnims()
    {
        animator.enabled=true;
        isPaused = false;
    }
    private void SetGrounded(bool isGrounded,float ySpeed)
    {
        if (isGrounded)
        {
            animator.SetTrigger("Grounded");
        }
        animator.SetBool("IsGrounded",isGrounded);
    }
    void Update()   
    {
        if (isDead||isPaused) return;
        if (_input.FrameInput.Growth)
        {
            animator.SetTrigger("Growth");
        }
    /*    if (_input.FrameInput.AttackDown)
        {
            
        }*/
        animator.SetFloat("Jump", playerController.Speed.y);
        animator.SetFloat("Speed", Mathf.Abs(playerController.Speed.x));
        RotateChar();
    }
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    public void Growth()
    {
       
    }
    private void RotateChar()
    {
        if (playerController.Speed.x < 0 && facingRight)
        {
            StartCoroutine(ObjectRotate(transform.rotation, Quaternion.Euler(new Vector3(0,leftAngle,0))));
            facingRight = false;
            facingVector = -1;
        }
        else
           if (playerController.Speed.x > 0 && !facingRight)
        {
            StartCoroutine(ObjectRotate(transform.rotation, Quaternion.Euler(new Vector3(0, rightAngle, 0))));
            facingRight = true;
            facingVector = 1;
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
}