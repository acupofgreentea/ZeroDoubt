using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    public Enemy CurrentEnemy { get; set; }
    
    public int SkillIndex { get; private set; }

    [SerializeField] private TextMeshProUGUI attackDamageText;
    [SerializeField] private TextMeshProUGUI healAmountText;

    protected override void Start()
    {
        base.Start();

        attackDamageText.text = Damage.ToString();
        healAmountText.text = healAmount.ToString();
    }

    public void ChooseAnEnemy(int index)
    {
        if (battleSystem.BattleState != BattleState.PlayerTurn) return;

        SkillIndex = index;

        battleSystem.ChangeGeneralText("Choose an Enemy to Attack!");
    }

    public void Attack()
    {
        if (battleSystem.BattleState != BattleState.PlayerTurn) return;

        if (TurnCompleted) return;

        var isDead = CurrentEnemy.TakeDamage(Damage);

        battleSystem.ChangeGeneralText("You Dealt " + Damage + " Damage to Enemy! Good Job");

        TurnCompleted = true;

        if (isDead)
        {
            battleSystem.EnemiesList.Remove(CurrentEnemy);
            CurrentEnemy.gameObject.SetActive(false);
        }

        StartCoroutine(TurnChangeRoutine());
    }
    
    public void Heal()
    {
        if (battleSystem.BattleState != BattleState.PlayerTurn) return;

        if (TurnCompleted) return;

        if (CurrentHp == MaxHp)
            battleSystem.ChangeGeneralText("You are on Max HP. You can't heal yourself! Choose an another action.");
        else
        {
            if (MaxHp >= CurrentHp + healAmount)
            {
                battleSystem.ChangeGeneralText("You Healed " + healAmount + " Yourself. Wise Choice!");
                CurrentHp += healAmount;
            }
            else
            {
                battleSystem.ChangeGeneralText("You Healed " + (MaxHp - CurrentHp) + " Yourself. Wise Choice!");
                CurrentHp = MaxHp;
            }
            
            SetHealthBar();

            
            
            StartCoroutine(TurnChangeRoutine());
        }
    }

    public void DebuffEnemy(int damageDecrease)
    {
        if (battleSystem.BattleState != BattleState.PlayerTurn) return;

        if (TurnCompleted) return;

        if (CurrentEnemy.Damage >= 0)
            CurrentEnemy.Damage -= damageDecrease;
        else
            CurrentEnemy.Damage = 0;
        
        battleSystem.ChangeGeneralText(CurrentEnemy.CharacterName + "'s damage has " + damageDecrease + " decreased. Now They are pissed!!");

        TurnCompleted = true;

        StartCoroutine(TurnChangeRoutine());
    }
    
    public IEnumerator TurnChangeRoutine()
    {
        yield return new WaitForSeconds(2f);

        TurnCompleted = false;
        
        if (battleSystem.EnemiesList.Count != 0)
        {
            battleSystem.ChangeGeneralText("Enemy Turn");
            battleSystem.BattleState = BattleState.EnemyTurn;
            battleSystem.EnemyTurn();
        }
        else
        {
            battleSystem.BattleState = BattleState.Won;
            battleSystem.ChangeGeneralText("You won the game!");
        }
    }
}