using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Enemy CurrentEnemy { get; set; }
    
    protected override void Start()
    {
        base.Start();
    }

    public void ChooseAnEnemyToAttack()
    {
        if (battleSystem.BattleState != BattleState.PlayerTurn) return;
        
        battleSystem.ChangeGeneralText("Choose an Enemy to Attack!");
    }

    public IEnumerator PlayerAttack()
    {
        if (battleSystem.BattleState != BattleState.PlayerTurn) yield break;
        
        if(TurnCompleted) yield break;

        var isDead = CurrentEnemy.TakeDamage(Damage);
        
        battleSystem.ChangeGeneralText("You Dealt " + Damage + " Damage to Enemy! Good Job");

        TurnCompleted = true;

        if (isDead)
        {
            battleSystem.EnemiesList.Remove(CurrentEnemy);
            CurrentEnemy.gameObject.SetActive(false);
        }

        yield return new WaitForSeconds(2f);
        
        TurnCompleted = false;
        
        if (isDead)
        {
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
        else
        {
            battleSystem.BattleState = BattleState.EnemyTurn;
            battleSystem.ChangeGeneralText("Enemy Turn");
            battleSystem.EnemyTurn();
        }
    }
    
    public void HealPlayer()
    {
        if (battleSystem.BattleState != BattleState.PlayerTurn) return;
        
        if(TurnCompleted) return;
        
        if(CurrentHP == MaxHP)
            battleSystem.ChangeGeneralText("You are on Max HP. You can't heal yourself! Choose an another action.");
        else
        {
            StartCoroutine(HealPlayerRoutine());
        }
    }

    private IEnumerator HealPlayerRoutine()
    {
        if (MaxHP >= CurrentHP + healAmount)
            CurrentHP += healAmount;
        else
            CurrentHP = MaxHP;
        
        SetHealthBar();
        
        battleSystem.ChangeGeneralText("You Healed " + healAmount + " Yourself. Wise Choice!");
        
        yield return new WaitForSeconds(2f);
        
        battleSystem.BattleState = BattleState.EnemyTurn;
        battleSystem.ChangeGeneralText("Enemy Turn");

        TurnCompleted = false;
        
        battleSystem.EnemyTurn();
    }
}
