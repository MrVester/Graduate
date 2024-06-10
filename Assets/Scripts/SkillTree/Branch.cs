using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Branch : MonoBehaviour
{
    [SerializeField]private Sprite Empty_Sprite;
    private Image BranchMain_Image;
    public List<GameObject> SubBranches;
    private SkillTree skillTree;
    private Skills currentSkill; 
    private void Awake()
    {
        //Make dynamically search for SubBranch buttons in this Go
        skillTree = GetComponentInParent<SkillTree>();
        BranchMain_Image  = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(UnequipSkill);
       foreach (GameObject subbranch in SubBranches)
        {
            subbranch.GetComponent<Button>().onClick.AddListener(() => EquipSkill(subbranch));
        }
    }

    public void DisactivateBranch()
    {
        try
        {
         GetComponent<Button>().interactable = false;
        }
        catch (Exception e)
        {
            print(e);
        }
       
    }
    public void ActivateBranch()
    {
        GetComponent<Button>().interactable = true;
    }
    public void SetSkillsActive(Skills skills)
    {
        foreach (GameObject subbranch in SubBranches)
        {
            var tmpskill = skills&subbranch.GetComponent<Skill>().skill;
            if (tmpskill!=0)
            {
                subbranch.GetComponent<Skill>().ActivateSkill();
            }
        }
    }
    public void DisactivateAllButtons()
    {
        DisactivateBranch();
        foreach (GameObject subbranch in SubBranches)
        {
            subbranch.GetComponent<Skill>().DisactivateSkill();
        }
    }
    public void ActivateAllButtons()
    {
        ActivateBranch();
        foreach (GameObject subbranch in SubBranches)
        {
            subbranch.GetComponent<Skill>().ActivateSkill();
        }
    }
    void EquipSkill(GameObject go)
    {   
        var tmpskill = go.GetComponent<Skill>().skill;
        skillTree.EquipSkill(tmpskill, currentSkill);
        SetSprite(go.GetComponent<Image>());
        currentSkill= tmpskill;
    }
    public void LoadSkill(Skills skill)
    {   
        foreach(GameObject subbranch in SubBranches)
        { 
            if(subbranch.GetComponent<Skill>().skill==skill)
            {
                //print("SkillLoaded: "+(int)skill);
                SetSprite(subbranch.GetComponent<Image>());
            }
            
        }
        //Image imageToSet=new Image;
        currentSkill = skill;
    }
    public void SetEmpty()
    {
        currentSkill = 0;
        RemoveSprite();
    }
    void UnequipSkill()
    {
        skillTree.UnequipSkill(currentSkill);
        currentSkill = 0;
        RemoveSprite();
    }
    void RemoveSprite()
    {
        BranchMain_Image.sprite = Empty_Sprite;
    }
    void SetSprite(Image image)
    {
        BranchMain_Image.sprite = image.sprite;
    }

}
