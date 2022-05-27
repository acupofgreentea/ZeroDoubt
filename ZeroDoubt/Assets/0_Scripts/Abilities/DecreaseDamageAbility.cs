using UnityEngine;

[CreateAssetMenu(fileName = "DecreaseDamage", menuName = "Ability/DecreaseDamage")]
public class DecreaseDamageAbility : AbilitySO
{
    [field: SerializeField] public int DamageToDecrease { get; set; }

    public override void Perform(Character character)
    {
        var battleSystem = character.BattleSystem;

        if (character.CharacterTypes == CharacterTypes.Player)
            if (battleSystem.BattleState != BattleState.PlayerTurn) return;

        if (character.CharacterTypes == CharacterTypes.Enemy)
            if (battleSystem.BattleState != BattleState.EnemyTurn) return;


        if (character.TurnCompleted) return;

        if (character.CurrentEnemy.Damage > DamageToDecrease)
        {
            battleSystem.ChangeGeneralText(character.CharacterName + " has decreased " + character.CurrentEnemy.CharacterName + "damage by " + DamageToDecrease + "!");

            character.CurrentEnemy.Damage -= DamageToDecrease;

            character.CurrentEnemy.UpdateStatsTexts();

            character.CurrentEnemy.UpdateAbilityText($"-{DamageToDecrease} Damage");

            character.TurnCompleted = true;

            var routine = character.TurnChangeRoutine();

            character.StartCoroutine(routine);
        }
        else
        {
            if(character.CharacterTypes == CharacterTypes.Player)
                battleSystem.ChangeGeneralText(character.CharacterName + " are no longer able to decrease " + character.CurrentEnemy.CharacterName + "'s damage. Choose an another Action.");
            if (character.CharacterTypes == CharacterTypes.Enemy)
                character.GetComponent<Enemy>().ChooseBehaviour();
        }

    }
}
