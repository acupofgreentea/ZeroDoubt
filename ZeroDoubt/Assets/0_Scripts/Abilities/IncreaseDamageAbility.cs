using UnityEngine;

[CreateAssetMenu(fileName = "IncreaseDamage", menuName = "Ability/IncreaseDamage")]
public class IncreaseDamageAbility : AbilitySO
{
    [field: SerializeField] public int DamageToIncrease { get; set; }

    public override void Perform(Character character)
    {
        var battleSystem = character.BattleSystem;

        if (character.CharacterTypes == CharacterTypes.Player)
            if (battleSystem.BattleState != BattleState.PlayerTurn) return;

        if (character.CharacterTypes == CharacterTypes.Enemy)
            if (battleSystem.BattleState != BattleState.EnemyTurn) return;


        battleSystem.ChangeGeneralText(character.CharacterName + " has increased themselves damage by " + DamageToIncrease + "!");

        character.UpdateAbilityText($"+{DamageToIncrease} Damage");

        character.Damage += DamageToIncrease;

        character.UpdateStatsTexts();

        character.TurnCompleted = true;

        var routine = character.TurnChangeRoutine();

        character.StartCoroutine(routine);
    }
}
