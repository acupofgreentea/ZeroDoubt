using UnityEngine;

[CreateAssetMenu(fileName = "Heal", menuName = "Ability/Heal")]

public class HealAbility : AbilitySO
{
    public override void Perform(Character character)
    {
        var battleSystem = character.BattleSystem;

        if (character.CharacterTypes == CharacterTypes.Player)
            if (battleSystem.BattleState != BattleState.PlayerTurn) return;

        if (character.CharacterTypes == CharacterTypes.Enemy)
            if (battleSystem.BattleState != BattleState.EnemyTurn) return;


        if (character.TurnCompleted) return;

        
        if (character.CurrentHp == character.MaxHp)
            character.BattleSystem.ChangeGeneralText(character.CharacterName + " are on Max HP." + character.CharacterName + " can't heal yourself! Choose an another action.");
        else
        {
            if (character.MaxHp >= character.CurrentHp + character.HealAmount)
            {
                character.BattleSystem.ChangeGeneralText(character.CharacterName + " Healed " + character.HealAmount + " themselves. Wise Choice!");
                character.UpdateAbilityText($"+{character.HealAmount} Health");
                character.CurrentHp += character.HealAmount;
            }
            else
            {
                character.BattleSystem.ChangeGeneralText(character.CharacterName + " Healed " + (character.MaxHp - character.CurrentHp) + " themselves. Wise Choice!");
                character.UpdateAbilityText($"+{character.MaxHp - character.CurrentHp} Health");
                character.CurrentHp = character.MaxHp;
            }
            
            character.TurnCompleted = true;
            
            character.SetHealthBar();

            var routine = character.TurnChangeRoutine();

            character.StartCoroutine(routine);
        }
    }
}