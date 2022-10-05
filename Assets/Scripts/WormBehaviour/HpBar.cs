using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image _redFillament; 
      
    private void Start()
    {
        _redFillament.fillAmount = 1f;
    }

    public void UpdateHealthBar(float fillAmount)
    {
        _redFillament.fillAmount = fillAmount;
    }
}
