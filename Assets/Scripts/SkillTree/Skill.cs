using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [Flags]
    public enum Skills:int
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
    }
    [Tooltip("Enum Enumov")]
    public Skills skill;



    private void UpdateSkills()
    {

    }

    void Update()
    {
        
    }
}
