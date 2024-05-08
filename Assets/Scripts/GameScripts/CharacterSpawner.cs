using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mainCharacter;
    [SerializeField] private Vector3 offsetSpawn=Vector3.zero;
    private Vector3 positionToSpawn=Vector3.zero;
    
    private void Awake()
    {
      
       
    }
    private void Start()
    {
        GameEvents.current.onCheckpointSavePosition += SaveLoadPosition;
        if (!JSONSave.HasKey("LastCheckpoint"))
        {
            positionToSpawn = Vector3.zero;
        }
        else
        {
            positionToSpawn = JSONSave.GetVector3("LastCheckpoint") + offsetSpawn;
        }
        mainCharacter.transform.position = new Vector3(positionToSpawn.x, positionToSpawn.y,mainCharacter.transform.position.z);
    }
    private void SaveLoadPosition(Vector3 pos)
    {
        JSONSave.SetVector3("LastCheckpoint", pos);
    }
    private void OnDestroy()
    {
        GameEvents.current.onCheckpointSavePosition -= SaveLoadPosition;
    }
}
