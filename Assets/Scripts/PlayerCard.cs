using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private Image _healthBar;
    [SerializeField] private Button _button;
    private int _maxHealth;
    private int _minHealth;

    private void Start()
    {
        _maxHealth = 10;
        _button.onClick.AddListener(DoStuff);
        SetUp(_maxHealth);
    }

    public void SetUp(int maxHealth,int health=0, int minHealth=0)
    {
        _maxHealth = maxHealth;
        _minHealth = minHealth;
        if (health == 0)
        {
            UpdateHealthBar(_maxHealth);
        }
        else
        {
            UpdateHealthBar(health);
        }
    }

    public void DoStuff()
    {
        Debug.Log("Did stuff");
        UpdateHealthBar(_maxHealth);
    }
    
    public void UpdateHealthBar(int health)
    {
        _healthBar.fillAmount = 5 / _maxHealth - _minHealth;
    }
}