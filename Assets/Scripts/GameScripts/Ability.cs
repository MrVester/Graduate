using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] private Skills ability;
    private void Start()
    {
       var abilities = (Skills)JSONSave.GetInt("Abilities");
        var tmpability = abilities & ability;
        if (tmpability !=0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<AbilitiesController>().EquipAbility(ability);
            Destroy(gameObject);
        }
    }
}
