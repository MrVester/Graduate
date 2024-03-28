using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Branch : MonoBehaviour
{
   
    private Image BranchMain_Image;
    public List<Button> SubBranches;
    private SkillTree skillTree;
    private Skill.Skills currentSkill; 
    private void Awake()
    {
        //Make dynamically search for SubBranch buttons in this Go
        skillTree = GetComponentInParent<SkillTree>();
        BranchMain_Image  = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(UnequipSkill); ;
       foreach (Button button in SubBranches)
        {
            button.onClick.AddListener(() => EquipSkill(button.gameObject));
        }
    }
    void EquipSkill(GameObject go)
    {   
        var tmpskill = go.GetComponent<Skill>().skill;
        skillTree.EquipSkill(tmpskill, currentSkill);
        SetSprite(go.GetComponent<Image>());
        currentSkill= tmpskill;
    }
    void UnequipSkill()
    {
        skillTree.UnequipSkill(currentSkill);
        currentSkill = 0;
        RemoveSprite();
    }
    void RemoveSprite()
    {
        BranchMain_Image.sprite = GetComponentInParent<SkillTree>().Empty_Sprite;
    }
    void SetSprite(Image image)
    {
        BranchMain_Image.sprite = image.sprite;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
