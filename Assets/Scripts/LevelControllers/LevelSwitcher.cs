using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSwitcher : MonoBehaviour
{
    //FIX THIS CLASS COMPLETELY
    public GameObject activeFrame;
    private void Awake()
    {
        activeFrame?.SetActive(false);
    }
   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            activeFrame?.SetActive(true);
            GameEvents.current.LevelColliderChanged(GetComponent<PolygonCollider2D>());
        }
    }*/
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            activeFrame?.SetActive(true);
            GameEvents.current.LevelColliderChanged(GetComponent<PolygonCollider2D>());
        }
        /*GameObject tmpGO;
        if (other.CompareTag("Player")&&!isInside)
        {
            isInside = true;
            tmpGO = other.gameObject;
        }*/
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
          try
          {
          activeFrame?.SetActive(false);
          }
          catch (Exception e)
          {
              print(e);
          }

    }
}
