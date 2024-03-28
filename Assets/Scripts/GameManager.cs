using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();
        JSONSave.Start(JSONSaveConfig.GetConfig());
        //skills = (Skill.Skills)JSONSave.GetInt("Skills");
    }
    
   /* private Skill.Skills skills;
    public Skill.Skills GetSkill()
    {
        return skills;
    }
    public void SetSkill(Skill.Skills skill)
    {
        this.skills = skill;
    }*/
   
}
