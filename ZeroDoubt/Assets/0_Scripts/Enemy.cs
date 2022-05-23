using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

        SetPlayerSkill();
    }

    private void SetPlayerSkill()
    {
        switch (player.SkillIndex)
        {
            case 0:
                player.Attack();
                break;
            case 1:
                player.DebuffEnemy(1);
                break;
        }
    }

    public void ChooseBehaviour()
    {
        StartCoroutine(TurnChangeRoutine());
    }

    private void Attack()
    {
        if (TurnCompleted) return;
        
        battleSystem.ChangeGeneralText(this.CharacterName + " dealt You " + Damage + " Damage. Be Careful!");
        
        var isDead = player.TakeDamage(Damage);

        TurnCompleted = true;
    }

    public IEnumerator TurnChangeRoutine()
    {
        // call skill method here

        var skill = Random.Range(0, 2);

        switch (skill)
        {
            case 0:
                Attack();
                break;
            
            case 1:
                if(CurrentHp == MaxHp)
                   Attack();
                else
                    Heal(); 
                
                break;
        }
        
        yield return new WaitForSeconds(2f);
        
        if (player.CurrentHp <= 0)
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
        if (TurnCompleted) return;
        
        if (MaxHp >= CurrentHp + healAmount)
        {
            CurrentHp += healAmount;
            battleSystem.ChangeGeneralText(this.CharacterName + " healed themselves for " + healAmount + ".");
        }
        else
        {
            battleSystem.ChangeGeneralText(this.CharacterName + " healed themselves for " + (MaxHp - CurrentHp) + ".");
            CurrentHp = MaxHp;
        }
        
        SetHealthBar();
    }
}
