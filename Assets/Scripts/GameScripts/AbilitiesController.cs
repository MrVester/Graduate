using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDController;
using UnityEditor.Playables;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
    [SerializeField] private SkillTree skillTree;
    private PlayerController playerController;
    private PlayerHealthController healthController;
/*    [SerializeField] private bool DoubleJump;
    [SerializeField]private bool Vine;
    [SerializeField]private bool GreenHeart;
    [SerializeField]private bool Umbrella;
    public event Action<bool> DoubleJumpChanged;
    public event Action<bool> VineChanged;
    public event Action<bool> GreenHeartChanged;
    public event Action<bool> UmbrellaChanged;*/
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        healthController = GetComponent<PlayerHealthController>();
        skillTree.SkillsUpdated += SetAbilities;
    }
    public void Start()
    {
        
        skillTree.DisactivateAllButtons();
        

    }
    private void Update()
    {
       
    }
    private void SetAbilities((Skills red, Skills green, Skills blue) cort)
    {
        //abilities = (Skills)JSONSave.GetInt("Abilities");
        
        if ((cort.green & Skills.GreenSkill2) != 0)
        {
            /*Umbrella = true;
            UmbrellaChanged?.Invoke(true);*/
        }
        if ((cort.green & Skills.GreenSkill3) != 0)
        {
            healthController.StartGreenHeartHeal();
        }
        else
        {
            healthController.StopGreenHeartHeal();
        }
        if ((cort.blue & Skills.BlueSkill2) != 0)
        {
            playerController.SetDoubleJumps(true);
            
        }
        else
        {
            playerController.SetDoubleJumps(false);
        }
    }
    public void EquipAbility(Skills ability)
    {
        skillTree.EquipAbilities(ability);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("CheckPoint"))
        {
            GameEvents.current.CheckpointTouched();
            GameEvents.current.CheckpointSavePosition(collision.transform.position);
            skillTree.LoadAbilities();
            healthController.SetMaxHealth();
            
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CheckPoint"))
        {
            skillTree.DisactivateAllButtons();
        }
            
    }
}
