using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image redFillament; 
      
    private void Start()
    {
        redFillament.fillAmount = 1f;
    }

    public void UpdateHealthBar(float fillAmount)
    {
        redFillament.fillAmount = fillAmount;
    }
}
