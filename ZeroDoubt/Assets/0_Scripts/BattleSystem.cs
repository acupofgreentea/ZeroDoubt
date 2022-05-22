using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleSystem : MonoBehaviour
{
    [field: SerializeField] public BattleState BattleState { get; set; }

    [SerializeField] private TextMeshProUGUI gameGeneralText;

    [SerializeField] private Character player;
    
    public Enemy CurrentEnemy { get; set; }

    private void Start()
    {
        BattleState = BattleState.Start;
        
        StartCoroutine(PlayerTurn());
    }

    private IEnumerator PlayerTurn()
    {
        ChangeGeneralText("Fight Begins. Stay Strong");
        
        yield return new WaitForSeconds(2f);
        
        BattleState = BattleState.PlayerTurn;
        
        ChangeGeneralText("Your Turn. Choose an Action");
    }

    // public IEnumerator PlayerAttack()
    // {
    //     if (BattleState != BattleState.PlayerTurn) yield break;
    //
    //     var isDead = CurrentEnemy.TakeDamage(player.Damage);
    //     
    //     ChangeGeneralText("You Dealt " + player.Damage + " Damage to Enemy!. Good Job");
    //     
    //     yield return new WaitForSeconds(2f);
    //
    //     if (isDead)
    //     {
    //         if (FindObjectsOfType<Enemy>() != null)
    //             BattleState = BattleState.EnemyTurn;
    //         else
    //             BattleState = BattleState.Won;
    //     }
    //     else
    //     {
    //         BattleState = BattleState.EnemyTurn;
    //         ChangeGeneralText("Enemy Turn");
    //     }
    // }
    //
    // public void ChooseAnEnemyToAttack()
    // {
    //     if (BattleState != BattleState.PlayerTurn) return;
    //     
    //     ChangeGeneralText("Choose an Enemy to Attack!");
    // }
    //
    // public void HealPlayer()
    // {
    //     //heal player 
    //     //change game state
    //
    //     if (BattleState != BattleState.PlayerTurn) return;
    //     
    //     BattleState = BattleState.EnemyTurn;
    // }

    public void ChangeGeneralText(string message)
    {
        gameGeneralText.text = message;
    }
}
