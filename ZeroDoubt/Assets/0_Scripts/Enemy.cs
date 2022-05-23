using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Character, IAttack, IHeal
{
    private Player player;
    
    [field: SerializeField] public int HealAmount { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    
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


    public void Attack()
    {
        if (TurnCompleted) return;
        
        battleSystem.ChangeGeneralText(this.CharacterName + " dealt You " + Damage + " Damage. Be Careful!");
        
        var isDead = player.TakeDamage(Damage);

        TurnCompleted = true;
    }
    public void Heal()
    {
        if (TurnCompleted) return;
        
        if (MaxHp >= CurrentHp + HealAmount)
        {
            CurrentHp += HealAmount;
            battleSystem.ChangeGeneralText(this.CharacterName + " healed themselves for " + HealAmount + ".");
        }
        else
        {
            battleSystem.ChangeGeneralText(this.CharacterName + " healed themselves for " + (MaxHp - CurrentHp) + ".");
            CurrentHp = MaxHp;
        }
        
        SetHealthBar();
    }
    
    
    private IEnumerator TurnChangeRoutine()
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
}
