using System.Collections;
using System.Collections.Generic;
using TDController;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public string sceneToLoad="MainMenu";
    public bool LoadSceneOnAwake=true;
    protected override void Awake()
    {
        base.Awake();
        JSONSave.Start(JSONSaveConfig.GetConfig());
        if(LoadSceneOnAwake)
        SceneManager.LoadScene(sceneToLoad);
    }
}
