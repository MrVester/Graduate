using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    public GameObject activeFrame;
    private void Awake()
    {
        activeFrame.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            activeFrame?.SetActive(true);
            GameEvents.current.LevelColliderChanged(GetComponent<PolygonCollider2D>());
        }
    }
}
