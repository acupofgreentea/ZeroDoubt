using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected string characterName;
    [SerializeField] protected int characterLevel;
    [SerializeField] protected TextMeshProUGUI characterNameText;
    [SerializeField] protected TextMeshProUGUI characterLevelText;
    [SerializeField] protected int healAmount;

    [field: SerializeField] public int Damage { get; private set; }
    
    [SerializeField] protected BattleSystem battleSystem;
    
    
    [field: SerializeField] public int CurrentHP { get; set;}
    [field: SerializeField] public int MaxHP { get; set; }

    protected virtual void Start()
    {
        CurrentHP = MaxHP;
        characterNameText.text = characterName;
        characterLevelText.text = "Lvl." + characterLevel.ToString();
    }

    public bool TakeDamage(int dmg)
    {
        CurrentHP -= dmg;

        if (CurrentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
