using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SkillTree : MonoBehaviour
{
   public Sprite Empty_Sprite;
   public Image Heart_Image;
   public GameObject RedBranchMain;
   public GameObject BlueBranchMain;
   public GameObject GreenBranchMain;
    //private Skill
    [SerializeField] Skill.Skills skills;

    private void Awake()
    {
        //print(JSONSave.GetInt("Skills"));
    }
    private void Start()
    {
        LoadSkills();
    }
    public void LoadSkills()
    {
        skills = (Skill.Skills)JSONSave.GetInt("Skills");

        //Red
        var tmpskills = (skills & Skill.Skills.RedSkill1) | (skills & Skill.Skills.RedSkill2) | (skills & Skill.Skills.RedSkill3);

        if (tmpskills!=0)
        {
            RedBranchMain.GetComponent<Branch>().LoadSkill(tmpskills);
        }
        else
            RedBranchMain.GetComponent<Branch>().SetEmpty();

        //Green
        tmpskills = (skills & Skill.Skills.GreenSkill1) | (skills & Skill.Skills.GreenSkill2) | (skills & Skill.Skills.GreenSkill3);
        if (tmpskills != 0)
        {
            

            GreenBranchMain.GetComponent<Branch>().LoadSkill(tmpskills);
        }
        else
            GreenBranchMain.GetComponent<Branch>().SetEmpty();

        //Blue
        tmpskills = (skills & Skill.Skills.BlueSkill1) | (skills & Skill.Skills.BlueSkill2) | (skills & Skill.Skills.BlueSkill3);
        if (tmpskills != 0)
        {
            BlueBranchMain.GetComponent<Branch>().LoadSkill(tmpskills);
        }
        else
            BlueBranchMain.GetComponent<Branch>().SetEmpty();
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
