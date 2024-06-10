using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
   [SerializeField] private Image Heart_Image;
   [SerializeField] private Branch RedBranchMain;
   [SerializeField] private Branch BlueBranchMain;
   [SerializeField] private Branch GreenBranchMain;
   private Skills currentRedSkill;
   private Skills currentGreenSkill;
   private Skills currentBlueSkill;

   public event Action<(Skills, Skills, Skills)> SkillsUpdated;
    [SerializeField] Skills skills;
    [SerializeField] Skills abilities;

    private void Start()
    {
        LoadSkills();
        LoadAbilities();
        DisactivateAllButtons();
    }
    public void DisactivateAllButtons()
    {
       RedBranchMain.DisactivateAllButtons();
       GreenBranchMain.DisactivateAllButtons();
       BlueBranchMain.DisactivateAllButtons();
    }
    public void ActivateAllButtons()
    {
        RedBranchMain.ActivateAllButtons();
        GreenBranchMain.ActivateAllButtons();
        BlueBranchMain.ActivateAllButtons();
    }
    public void LoadAbilities()
    {
        DisactivateAllButtons();
        abilities = (Skills)JSONSave.GetInt("Abilities");
        var tmpabilities = abilities & (Skills.RedSkill1 | Skills.RedSkill2 | Skills.RedSkill3);
        if (tmpabilities != 0)
        {
            RedBranchMain.ActivateBranch();
            RedBranchMain.SetSkillsActive(tmpabilities);
        }

        tmpabilities = abilities & (Skills.GreenSkill1 | Skills.GreenSkill2 | Skills.GreenSkill3);
        if (tmpabilities != 0)
        {
            GreenBranchMain.ActivateBranch();
            GreenBranchMain.SetSkillsActive(tmpabilities);
        }

        tmpabilities = abilities & (Skills.BlueSkill1 | Skills.BlueSkill2 | Skills.BlueSkill3);
        if (tmpabilities != 0)
        {
            BlueBranchMain.ActivateBranch();
            BlueBranchMain.SetSkillsActive(tmpabilities);
        }

    }
    public void LoadSkills()
    {
        skills = (Skills)JSONSave.GetInt("Skills");

        //Red
        var tmpskills = skills & (Skills.RedSkill1 | Skills.RedSkill2 | Skills.RedSkill3);

        if (tmpskills!=0)
        {
            RedBranchMain.LoadSkill(tmpskills);
            currentRedSkill = tmpskills;
        }
        else
        {
            RedBranchMain.SetEmpty();
            currentRedSkill=Skills.EmptySkill;
        }
            

        //Green
        tmpskills = skills & (Skills.GreenSkill1 | Skills.GreenSkill2 | Skills.GreenSkill3);
        if (tmpskills != 0)
        {
            

            GreenBranchMain.LoadSkill(tmpskills);
            currentGreenSkill = tmpskills;
        }
        else
        {
            GreenBranchMain.SetEmpty();
            currentGreenSkill = Skills.EmptySkill;
        }

        //Blue
        tmpskills = skills & (Skills.BlueSkill1 | Skills.BlueSkill2 | Skills.BlueSkill3);
        if (tmpskills != 0)
        {
            BlueBranchMain.LoadSkill(tmpskills);
            currentBlueSkill = tmpskills;
        }
        else
        {
            BlueBranchMain.SetEmpty();
            currentBlueSkill = Skills.EmptySkill;
        }

        SkillsUpdated?.Invoke(GetCurrentSkills());
    }
    public (Skills, Skills, Skills) GetCurrentSkills()
    {
        return (currentRedSkill, currentGreenSkill, currentBlueSkill);
    }
    public void EquipAbilities(Skills abilities)
    {
        print("CRNT_AB: "+(int)this.abilities+" "+"NEW_AB: "+ (int)abilities);
        this.abilities|=abilities;
        print("FINAL: " + (int)this.abilities);
        JSONSave.SetInt("Abilities", (int)this.abilities);
    }
    public void EquipSkill(Skills newskill, Skills oldskill)
    {
        skills = (skills^oldskill)|newskill;
        JSONSave.SetInt("Skills", (int)skills);
        LoadSkills();
    }
    public void UnequipSkill(Skills skill)
    {
        skills = skills ^ skill;
        JSONSave.SetInt("Skills",(int)skills);
        LoadSkills();
    }
}
