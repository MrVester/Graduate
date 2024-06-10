using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TDController;
using UnityEngine;

public class AbilitiesController : MonoBehaviour
{
    [SerializeField] private SkillTree skillTree;
    [SerializeField] private GameObject umbrellaLeaf;
    private PlayerController playerController;
    private PlayerHealthController healthController;
    private RobotAttack robotAttack;
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        healthController = GetComponent<PlayerHealthController>();
        robotAttack = GetComponent<RobotAttack>();
        skillTree.SkillsUpdated += SetAbilities;
    }
    public void Start()
    {
        skillTree.DisactivateAllButtons();

    }
    private void SetAbilities((Skills red, Skills green, Skills blue) cort)
    {
        if ((cort.red & Skills.RedSkill3) != 0)
        {
            robotAttack.SetBuffedDamage();

        }
        else
        {
            robotAttack.SetDefaultDamage();
        }
        if ((cort.green & Skills.GreenSkill2) != 0)
        {
            umbrellaLeaf.SetActive(true);
        }
        else
        {
            umbrellaLeaf.SetActive(false);
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
