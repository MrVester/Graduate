using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] GameObject deathScreen;
    private void Start()
    {
        GameEvents.current.onDeath += ActivateDeathScreen;
    }
    private void OnEnable()
    {
        deathScreen.SetActive(false); 
    }
    private void ActivateDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
