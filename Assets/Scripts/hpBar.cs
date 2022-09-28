using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hpBar : MonoBehaviour
{
    [SerializeField]  WormController wormController;
    private Stats stats;
    private float filledAmount;
    [SerializeField] private Image redFillament;

    private void Start()
    {
        filledAmount = 100;
    }

    void FixedUpdate()
    {
        filledAmount = wormController.stats.GetNormalizedHp();
        redFillament.fillAmount -= filledAmount;
    }

}
