using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public enum SkillColor
{
    Red,
    Green,
    Blue
}
public class PlayerSkills : MonoBehaviour
{
    [SerializeField]Skills RedSkill;
    [SerializeField]Skills GreenSkill;
    [SerializeField]Skills BlueSkill;
    public SkillTree skillTree;
    private void Awake()
    {
        skillTree.SkillsUpdated += SetPlayerSkills;
    }
    
    public Skills GetSkill(SkillColor color)
    {
        switch(color)
    {
        case SkillColor.Red:
                return RedSkill;
            case SkillColor.Green:
                return GreenSkill;
            case SkillColor.Blue:
                return BlueSkill;
            default:
                return Skills.EmptySkill;
            }
        }
    public void SetPlayerSkills((Skills redSkill, Skills greenSkill, Skills blueSkill) cort)
    {
        RedSkill = cort.redSkill;
        GreenSkill = cort.greenSkill;
        BlueSkill = cort.blueSkill;
    }
    private void OnDisable()
    {
        skillTree.SkillsUpdated -= SetPlayerSkills;

    }
    private void OnDestroy()
    {
        skillTree.SkillsUpdated -= SetPlayerSkills;
    }
}
