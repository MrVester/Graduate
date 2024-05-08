using System.Collections;
using System.Collections.Generic;
using TDController;
using UnityEngine;

public class VineBridge : MonoBehaviour
{
    private Animator animator;
    public Collider2D bridgeColl;
    private bool activated = false;
    private bool isInsideColl=false;
    private PlayerInput input;
    private GameObject player;
    private PlayerSkills playerSkills;
    private bool checkpointReached;
    private bool canResetBridge = false;

    // Start is called before the first frame update
    private void Awake()
    {
        input= GetComponent<PlayerInput>();
        player = FindObjectOfType<PlayerController>().gameObject;
        playerSkills=player.GetComponent<PlayerSkills>();
        animator = GetComponentInChildren<Animator>();
        DisableBridgeColl();
    }
    private void Start()
    {
        GameEvents.current.onCheckpointTouched += SetCheckPointReached;
    }
    private void OnEnable()
    {
        if (canResetBridge)
        {
            canResetBridge = false;
            ResetVineBridge();
        }
        else
        {
            if (activated)
            {
                animator.SetTrigger("Activated");
            }
           
        }

    }
    private void OnDestroy()
    {
        GameEvents.current.onCheckpointTouched -= SetCheckPointReached;
    }

    private void SetCheckPointReached()
    {
        if (!canResetBridge)
            canResetBridge = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (isInsideColl&&
            input.FrameInput.Growth&&
            playerSkills.GetSkill(SkillColor.Green)==Skills.GreenSkill1&&
            !activated)
        {
            activated = true;
            animator.SetTrigger("StartBridge");
        }
           
    }
    private void ResetVineBridge()
    {
        activated= false;
        DisableBridgeColl();
    }
    public void EnableBridgeColl()
    {
        bridgeColl.enabled = true;
    }
    public void DisableBridgeColl()
    {
        bridgeColl.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInsideColl = true;
        
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInsideColl = false;
    }
}
