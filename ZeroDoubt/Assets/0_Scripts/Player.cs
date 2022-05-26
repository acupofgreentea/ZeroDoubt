using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    private AbilitySO currentAbility;

    public void UseSkill()
    {
        currentAbility.Perform(this);
    }

    public void SetSkill(AbilitySO ability)
    {
        currentAbility = ability;
    }

    public void ChooseAnEnemy()
    {
        if (BattleSystem.BattleState != BattleState.PlayerTurn) return;

        BattleSystem.ChangeGeneralText("Choose an Enemy to Attack!");
    }
    
    public override IEnumerator TurnChangeRoutine()
    {
        var ab = currentAbility;

        yield return new WaitForSeconds(2f);

        TurnCompleted = false;
        
        if (BattleSystem.EnemiesList.Count != 0)
        {
            BattleSystem.ChangeGeneralText("Enemy Turn");
            BattleSystem.BattleState = BattleState.EnemyTurn;
            BattleSystem.EnemyTurn();
        }
        else
        {
            BattleSystem.BattleState = BattleState.Won;
            BattleSystem.ChangeGeneralText("You won the game!");
        }
    }

    
}