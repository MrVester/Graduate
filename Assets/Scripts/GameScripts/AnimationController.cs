using System.Collections;
using System.Collections.Generic;
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

    private bool facingRight=true;
    public bool isRotating = false;
    public bool isSpeed = false;
    // Start is called before the first frame update
    private void Awake()
    {
        playerController= _input.gameObject.GetComponent<PlayerController>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()   
    {
        if (_input.FrameInput.Growth)
        {
            animator.SetTrigger("Growth");
        }
        if (_input.FrameInput.AttackDown)
        {
            animator.SetTrigger("Attack");
        }
        animator.SetFloat("Jump", playerController.Speed.y);
        animator.SetFloat("Speed", Mathf.Abs(playerController.Speed.x));
        RotateChar();
    }
    private void RotateChar()
    {
        if (playerController.Speed.x < 0 && facingRight)
        {
            StartCoroutine(ObjectRotate(transform.rotation, Quaternion.Euler(new Vector3(0,leftAngle,0))));
            facingRight = false;
        }
        else
           if (playerController.Speed.x > 0 && !facingRight)
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
}