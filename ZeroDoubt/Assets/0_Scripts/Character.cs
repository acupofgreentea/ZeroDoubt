using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [Header("Character Info")]
    [SerializeField] protected string characterName;
    [SerializeField] protected int characterLevel;
    
    [Header("Character Stats")]
    
    [SerializeField] protected int healAmount;
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public int CurrentHP { get; set;}
    [field: SerializeField] public int MaxHP { get; set; }
    
    [Header("References")]
    [SerializeField] protected TextMeshProUGUI characterNameText;
    [SerializeField] protected TextMeshProUGUI characterLevelText;

    [SerializeField] protected Image healthBar;

    [SerializeField] protected BattleSystem battleSystem;

    public bool TurnCompleted { get; set; } = false;
    
    
    protected virtual void Start()
    {
        CurrentHP = MaxHP;
        SetHealthBar();
        characterNameText.text = characterName;
        characterLevelText.text = "Lvl." + characterLevel.ToString();
    }

    public bool TakeDamage(int dmg)
    {
        CurrentHP -= dmg;
        
        SetHealthBar();

        if (CurrentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetHealthBar()
    {
        healthBar.fillAmount = (float)CurrentHP / MaxHP;
    }
}
