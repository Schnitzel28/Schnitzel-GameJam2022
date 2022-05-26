using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    DeathHandler deathHandler;
    TMP_Text healthDisplay;


    void Start()
    {
        deathHandler = FindObjectOfType<DeathHandler>();
        healthDisplay = GetComponent<TMP_Text>();
    }

    void Update()
    {
        healthDisplay.text = deathHandler.CurrentHealth.ToString();
    }
}
