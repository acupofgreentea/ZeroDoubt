using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class BattleSystem : MonoBehaviour
{
    [field: SerializeField] public BattleState BattleState { get; set; }

    [SerializeField] private TextMeshProUGUI gameGeneralText;

    [SerializeField] private Character player;

    public List<Character> EnemiesList { get; set; } = new List<Character>();

    public Character EnemyToAttack { get; set; }

    private void Start()
    {
        BattleState = BattleState.Start;
        
        StartCoroutine(GameStart());

        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            EnemiesList.Add(enemy);
        }
    }

    private IEnumerator GameStart()
    {
        ChangeGeneralText("Fight Begins. Stay Strong");
        
        yield return new WaitForSeconds(2f);
        
        BattleState = BattleState.PlayerTurn;
        
        ChangeGeneralText("Your Turn. Choose an Action");
    }

    public void EnemyTurn()
    {
        if(BattleState != BattleState.EnemyTurn) return;

        StartCoroutine(ChooseEnemy());
    }
    
    private IEnumerator ChooseEnemy()
    {
        yield return new WaitForSeconds(1f);
        
        var enemyIndex = Random.Range(0, EnemiesList.Count);
        
        EnemyToAttack = EnemiesList[enemyIndex];

        var routine = EnemyToAttack.TurnChangeRoutine();

        EnemyToAttack.StartCoroutine(routine);
    }

    public void ChangeGeneralText(string message)
    {
        gameGeneralText.text = message;
    }
}
