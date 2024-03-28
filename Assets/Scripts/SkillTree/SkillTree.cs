using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
   public Sprite Empty_Sprite;
   public Image Heart_Image;
   public Image RedBranchMain_Image;
   public Image BlueBranchMain_Image;
   public Image GreenBranchMain_Image;
    //private Skill
    [SerializeField] Skill.Skills skills;
    private void Awake()
    {
        LoadSkills();
        print(JSONSave.GetInt("Skills"));
    }
    private void Start()
    {
        RedBranchMain_Image.sprite = Empty_Sprite;
        BlueBranchMain_Image.sprite = Empty_Sprite;
        GreenBranchMain_Image.sprite = Empty_Sprite;
    }
    public void LoadSkills()
    {
        skills = (Skill.Skills)JSONSave.GetInt("Skills");
        //Blue
        if (skills.HasFlag(Skill.Skills.RedSkill1)|| skills.HasFlag(Skill.Skills.RedSkill2) || skills.HasFlag(Skill.Skills.RedSkill3))
        {

        }
        if (skills.HasFlag(Skill.Skills.GreenSkill1) || skills.HasFlag(Skill.Skills.GreenSkill2) || skills.HasFlag(Skill.Skills.GreenSkill3))
        {

        }
        if (skills.HasFlag(Skill.Skills.BlueSkill1) || skills.HasFlag(Skill.Skills.BlueSkill2) || skills.HasFlag(Skill.Skills.BlueSkill3))
        {

        }
    }
    public void GetActiveSkills()
    {
       
    }
    public void EquipSkill(Skill.Skills newskill, Skill.Skills oldskill)
    {   if(skills!=0)
        print("SkillSet changed from: "+skills.ToString());
        skills = (skills^oldskill)|newskill;
        JSONSave.SetInt("Skills", (int)skills);
    }
    public void UnequipSkill(Skill.Skills skill)
    {
        skills = skills ^ skill;
        JSONSave.SetInt("Skills",(int)skills);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
