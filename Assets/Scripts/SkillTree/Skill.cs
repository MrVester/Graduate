using System;
using UnityEngine;
using UnityEngine.UI;
[Flags]
public enum Skills : int
{
    EmptySkill = 0,
    RedSkill1 = 1 << 0,
    RedSkill2 = 1 << 1,
    RedSkill3 = 1 << 2,
    GreenSkill1 = 1 << 3,
    GreenSkill2 = 1 << 4,
    GreenSkill3 = 1 << 5,
    BlueSkill1 = 1 << 6,
    BlueSkill2 = 1 << 7,
    BlueSkill3 = 1 << 8,
    //All=111111111,
}
public class Skill : MonoBehaviour
{
    
    [Tooltip("Enum Enumov")]
    public Skills skill;
    public void ActivateSkill()
    {
        gameObject.GetComponent<Button>().interactable = true;
    }
    public void DisactivateSkill()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }
}

