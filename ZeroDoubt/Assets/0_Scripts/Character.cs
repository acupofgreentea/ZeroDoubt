using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] protected TextMeshProUGUI characterNameText;

    [SerializeField] protected TextMeshProUGUI characterLevelText;

    [SerializeField] protected TextMeshProUGUI healthText;

    [SerializeField] protected Image healthBar;

    [SerializeField] protected BattleSystem battleSystem;

    [SerializeField] private CharacterSO characterSO;
    
    
    
    public string CharacterName { get; private set; }
    public int CurrentHp { get; set; }
    protected int MaxHp { get; set; }
    
    
    protected bool TurnCompleted { get; set; } = false;
    
    protected int characterLevel;
    
    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        CharacterName = characterSO.CharacterName;
        characterLevel = characterSO.CharacterLevel;
        _spriteRenderer.sprite = characterSO.Sprite;
        
        CurrentHp = characterSO.CurrentHp;
        MaxHp = characterSO.MaxHp;
    }


    protected virtual void Start()
    {
        CurrentHp = MaxHp;
        SetHealthBar();
        characterNameText.text = CharacterName;
        characterLevelText.text = "Lvl." + characterLevel.ToString();
    }

    public bool TakeDamage(int dmg)
    {
        CurrentHp -= dmg;

        SetHealthBar();

        if (CurrentHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    protected void SetHealthBar()
    {
        healthBar.fillAmount = (float)CurrentHp / MaxHp;
        healthText.text = CurrentHp + " / " + MaxHp;
    }
}