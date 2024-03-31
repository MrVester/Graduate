using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    public GameObject settingsTab;
    public Button backButtonFromSettings;
    [Header("MainMenu")]
    public GameObject mainMenuTab;
    public Button playButton;
    public string gameSceneName="Game";
    public Button settingsButton;
    public Button quitButton;

    void Start()
    {
        playButton.onClick.AddListener(() => LoadLevel(gameSceneName));
        settingsButton.onClick.AddListener(() => SettingsButtonEvent());
        quitButton.onClick.AddListener(() => Application.Quit());
        backButtonFromSettings.onClick.AddListener(() => BackButtonFromSettingsEvent());

    }


    private void BackButtonFromSettingsEvent()
    {
        settingsTab.SetActive(false);
        mainMenuTab.SetActive(true);
    }
    private void SettingsButtonEvent()
    {
        settingsTab.SetActive(true);
        mainMenuTab.SetActive(false);
    }
    private void BackButtonFromLevelSelectorEvent()
    {
        mainMenuTab.SetActive(true);
    }

    private void LevelSelectionButtonEvent()
    {
        mainMenuTab.SetActive(false);
    }



    private void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
}
