using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BasicAttack", menuName = "Ability/BasicAttack")]
public class BasicAttackAbility : AbilitySO
{
    [field: SerializeField] public int Damage {get; set;}

    public override void Perform(Character character)
    {
        var battleSystem = character.BattleSystem;

        if(character.CharacterTypes == CharacterTypes.Player)
            if (battleSystem.BattleState != BattleState.PlayerTurn) return;

        if (character.CharacterTypes == CharacterTypes.Enemy)
            if (battleSystem.BattleState != BattleState.EnemyTurn) return;

        if (character.TurnCompleted) return;

        var isDead = character.CurrentEnemy.TakeDamage(Damage);


        battleSystem.ChangeGeneralText(character.CharacterName + " Dealt " + Damage + " Damage to " + character.CurrentEnemy.CharacterName + " !");

        character.TurnCompleted = true;

        if (isDead)
        {
            if(character.CharacterTypes == CharacterTypes.Player)
                character.BattleSystem.EnemiesList.Remove(character.CurrentEnemy);

            character.CurrentEnemy.gameObject.SetActive(false);
        }

        var routine = character.TurnChangeRoutine();

        character.StartCoroutine(routine);
    }
}
