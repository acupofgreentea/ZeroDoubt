using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Player player;
    
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnMouseDown()
    {
        player.CurrentEnemy = this;
        
        player.StartCoroutine(player.PlayerAttack());
    }

    public void ChooseBehaviour()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        if (TurnCompleted) yield break;
        
        battleSystem.ChangeGeneralText(this.characterName + " dealt You " + Damage + " Damage. Be Careful!");
        
        var isDead = player.TakeDamage(Damage);

        TurnCompleted = true;

        yield return new WaitForSeconds(2f);
        
        if (isDead)
        {
            battleSystem.BattleState = BattleState.Lost;
            battleSystem.ChangeGeneralText("You Lost :(");
        }
        else
        {
            battleSystem.BattleState = BattleState.PlayerTurn;
            battleSystem.ChangeGeneralText("Your Turn");
        }

        TurnCompleted = false;
    }

    private void Heal()
    {
        
    }
}
