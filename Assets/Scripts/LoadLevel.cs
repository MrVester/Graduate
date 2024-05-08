using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadLevel : MonoBehaviour
{
    [SerializeField] private string sceneToLoad="Game";
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(Load);
    }
    private void Load()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
