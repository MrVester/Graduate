using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTeleport : MonoBehaviour
{
    public Transform teleportTo;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position=new Vector3 (teleportTo.position.x, teleportTo.position.y, other.gameObject.transform.position.z);
            teleportTo.gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        this.gameObject.SetActive(true);
    }
}
