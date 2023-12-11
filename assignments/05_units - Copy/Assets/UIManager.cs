using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    public TMP_Text cooldown1;
    public TMP_Text cooldown2;

    public GameObject restart;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.SharedInstance.playerDead)
        {
            restart.SetActive(true);
        }

        if (GameManager.SharedInstance.lightiningCooldownTimer < 1f)
        {
            cooldown1.text = "Ready";
        }
        else
        {
            cooldown1.text = Math.Round(GameManager.SharedInstance.lightiningCooldownTimer).ToString();
        }

        if (GameManager.SharedInstance.fireCooldownTimer < 1f)
        {
            cooldown2.text = "Ready";
        }
        else
        {
            cooldown2.text = Math.Round(GameManager.SharedInstance.fireCooldownTimer).ToString();
        }
        
    }

}
