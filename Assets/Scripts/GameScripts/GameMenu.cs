using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TDController;
using PlayerInput = TDController.PlayerInput;

[RequireComponent(typeof(PlayerInput))]
public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject Menu;
    private PlayerInput _input;

    // Start is called before the first frame update
    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
    }
    void Start()
    {
        if (Menu.activeSelf)
            DisactivateMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.FrameInput.Menu)
        {
            print("ESC");
            HandleMenu();
        }
    }
    private void HandleMenu()
    {
        if (!Menu.activeSelf)
        {
            UIEvents.current.GameStop();
            ActivateMenu();
        }
        else
        {
            UIEvents.current.GameStart();
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

}
