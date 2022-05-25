using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Character
{
    [SerializeField] private List<AbilitySO> abilites;

    private Player player;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        this.CurrentEnemy = player;
    }
    
    private void OnMouseDown()
    {
        player.CurrentEnemy = this;
        
        player.UseSkill();
    }
    
    public void ChooseBehaviour()
    {
        StartCoroutine(TurnChangeRoutine());
    }

    public void ChooseAbility()
    {
        var skill = Random.Range(0, abilites.Count);

        if(CurrentHp == MaxHp)
        {
            if (skill == 0) skill++;
        }

        abilites[skill].Perform(this);
    }

    public override IEnumerator TurnChangeRoutine()
    {
        ChooseAbility();
        
        yield return new WaitForSeconds(2f);
        
        if (player.CurrentHp <= 0)
        {
            BattleSystem.BattleState = BattleState.Lost;
            BattleSystem.ChangeGeneralText("You Lost :(");
        }
        else
        {
            BattleSystem.BattleState = BattleState.PlayerTurn;
            BattleSystem.ChangeGeneralText("Your Turn");
        }

        TurnCompleted = false;
    }

    
}
