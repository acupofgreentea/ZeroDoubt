using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DecreaseDamage", menuName = "Ability/DecreaseDamage")]
public class DecreaseDamageAbility : AbilitySO
{
    [field: SerializeField] public int DamageToDecrease { get; set; }

    public override void Perform(Character character)
    {
        var battleSystem = character.BattleSystem;

        var player = character.CharacterTypes == CharacterTypes.Player;
        var enemy = character.CharacterTypes == CharacterTypes.Enemy;

        if (player)
            if (battleSystem.BattleState != BattleState.PlayerTurn) return;

        if (enemy)
            if (battleSystem.BattleState != BattleState.EnemyTurn) return;


        if (character.TurnCompleted) return;

        if (character.CurrentEnemy.Damage > DamageToDecrease)
        {
            battleSystem.ChangeGeneralText(character.CharacterName + " has decreased " + character.CurrentEnemy.CharacterName + "damage by " + DamageToDecrease + "!");
            character.CurrentEnemy.Damage -= DamageToDecrease;

            character.TurnCompleted = true;

            var routine = character.TurnChangeRoutine();

            character.StartCoroutine(routine);
        }
        else
        {
            if(player)
                battleSystem.ChangeGeneralText(character.CharacterName + " are no longer able to decrease " + character.CurrentEnemy.CharacterName + "'s damage. Choose an another Action.");
            if (enemy)
                character.GetComponent<Enemy>().ChooseBehaviour();
        }

    }
}
