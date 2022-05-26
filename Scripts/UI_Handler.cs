using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Handler : MonoBehaviour
{
    // Death Text in DeathHandler
    // Health Ammount in HealthDisplay
    // Health Text always visible
    [SerializeField] GameObject levelCompleteText;
    [SerializeField] GameObject escapeText;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject quitButton;

    [SerializeField] bool EscapeMenuActive = false;

    void Start()
    {
        
    }


    void Update()
    {
        EscapeMenu();


    }

    #region Escape
    void EscapeMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !EscapeMenuActive)
        {
            HandleEscapemenu(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && EscapeMenuActive)
        {
            HandleEscapemenu(false);
        }
    }

    void HandleEscapemenu(bool state)
    {
        EscapeMenuActive = state;
        HandleButtons(state);
        escapeText.SetActive(state);
        EscapeMenuActive = state;
    }

    #endregion

    #region EndScreen

    public void HandleEndscreen(bool state)
    {
        HandleButtons(state);
        levelCompleteText.SetActive(state);
    }

    #endregion

    #region General

    public void DisableUI ()
    {
        HandleButtons(false);
        escapeText.SetActive(false);
        levelCompleteText.SetActive(false);
    }

    void HandleButtons(bool state)
    {
        restartButton.SetActive(state);
        quitButton.SetActive(state);
    }

    #endregion
}
