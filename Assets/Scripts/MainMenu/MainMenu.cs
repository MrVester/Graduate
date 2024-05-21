using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject settingsTab;
    [SerializeField] private Button backButtonFromSettings;
    [Header("MainMenu")]
    [SerializeField] private GameObject mainMenuTab;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private string gameSceneName="Game";
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private RawImage continueButtonImage;
    [SerializeField] private TextMeshProUGUI continueButtonText;

    void Start()
    {
        newGameButton.onClick.AddListener(() =>StartNewGame());
        continueButton.onClick.AddListener(() => LoadLevel(gameSceneName));
        settingsButton.onClick.AddListener(() => SettingsButtonEvent());
        quitButton.onClick.AddListener(() => Application.Quit());
        backButtonFromSettings.onClick.AddListener(() => BackButtonFromSettingsEvent());
        SetContinueButtonState();
    }

    private void StartNewGame()
    {
        JSONSave.DeleteKey("Abilities");
        JSONSave.DeleteKey("Skills");
        JSONSave.DeleteKey("LastCheckpoint");
        LoadLevel(gameSceneName);
    }
    private void SetContinueButtonState()
    {
        if (!JSONSave.HasKey("LastCheckpoint"))
        {
            continueButtonImage.color = new Color32(128, 128, 128, 128);
            continueButtonText.color = new Color32(128, 128, 128, 255);
            continueButton.interactable = false;
        }
        else
        {
            continueButtonImage.color = new Color32(255, 255, 255, 128);
            continueButtonText.color = Color.white;
            continueButton.interactable = true;
        }
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
