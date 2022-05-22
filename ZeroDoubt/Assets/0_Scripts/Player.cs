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

        var isDead = CurrentEnemy.TakeDamage(Damage);
        
        battleSystem.ChangeGeneralText("You Dealt " + Damage + " Damage to Enemy!. Good Job");
        
        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            if (FindObjectsOfType<Enemy>() != null)
            {
                battleSystem.ChangeGeneralText("Enemy Turn");
                battleSystem.BattleState = BattleState.EnemyTurn;
            }
            else
                battleSystem.BattleState = BattleState.Won;
        }
        else
        {
            battleSystem.BattleState = BattleState.EnemyTurn;
            battleSystem.ChangeGeneralText("Enemy Turn");
        }
    }
    
    public void HealPlayer()
    {
        if (battleSystem.BattleState != BattleState.PlayerTurn) return;
        
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
        
        battleSystem.ChangeGeneralText("You Healed " + healAmount + " Yourself. Wise Choice!");
        
        yield return new WaitForSeconds(2f);
        
        battleSystem.BattleState = BattleState.EnemyTurn;
        battleSystem.ChangeGeneralText("Enemy Turn");
    }
}
