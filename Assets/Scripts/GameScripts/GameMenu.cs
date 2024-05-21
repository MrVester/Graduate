using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TDController;
using PlayerInput = TDController.PlayerInput;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInput))]
public class GameMenu : MonoBehaviour
{
    private PlayerInput _input;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Settings;
    [SerializeField] private Button openSettingsButton;
    [SerializeField] private Button backSettingsButton;
    [SerializeField] private List<GameObject> gOSToDisactivateOnSettings;

    // Start is called before the first frame update
    private void Awake()
    {
        openSettingsButton.onClick.AddListener(OpenSettings);
        backSettingsButton.onClick.AddListener(CloseSettings);
        _input = GetComponent<PlayerInput>();
    }
    void Start()
    {
        if (Menu.activeSelf)
            DisactivateMenu();
        if (Settings.activeSelf)
            CloseSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.FrameInput.Menu)
        {
            print("ESC");
            if (Settings.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                HandleMenu();
            }
            
        }
    }
    private void HandleMenu()
    {
        if (!Menu.activeSelf)
        {
            GameEvents.current.GameStop();
            ActivateMenu();
        }
        else
        {
            GameEvents.current.GameStart();
            DisactivateMenu();
        }
    }
    private void ActivateMenu()
    {
        Menu.SetActive(true);
    }
    private void DisactivateMenu()
    {
        Menu.SetActive(false);
    }
    private void OpenSettings()
    {
        Settings.SetActive(true);
        foreach(var gO in gOSToDisactivateOnSettings)
        {
            gO.SetActive(false);
        }
    }
    private void CloseSettings()
    {
        Settings.SetActive(false);
        foreach (var gO in gOSToDisactivateOnSettings)
        {
            gO.SetActive(true);
        }
    }

}
