using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private TextMeshProUGUI attackDamageText;
    [SerializeField] private TextMeshProUGUI healAmountText;
    [field: SerializeField] public int HealAmount { get; set; }
    
    [field: SerializeField] public int DamageToDecrease { get; set; }
    public int SkillIndex { get; private set; }

    private AbilitySO currentAbility;
    
    protected override void Start()
    {
        base.Start();

        attackDamageText.text = Damage.ToString();
        healAmountText.text = HealAmount.ToString();
    }
    
    public void UseSkill()
    {
        currentAbility.Perform(this);
    }

    public void SetSkill(AbilitySO ability)
    {
        currentAbility = ability;
    }

    public void ChooseAnEnemy(int index)
    {
        if (BattleSystem.BattleState != BattleState.PlayerTurn) return;

        SkillIndex = index;

        BattleSystem.ChangeGeneralText("Choose an Enemy to Attack!");
    }
    
    public override IEnumerator TurnChangeRoutine()
    {
        var ab = currentAbility;

        yield return new WaitForSeconds(2f);

        TurnCompleted = false;
        
        if (BattleSystem.EnemiesList.Count != 0)
        {
            BattleSystem.ChangeGeneralText("Enemy Turn");
            BattleSystem.BattleState = BattleState.EnemyTurn;
            BattleSystem.EnemyTurn();
        }
        else
        {
            BattleSystem.BattleState = BattleState.Won;
            BattleSystem.ChangeGeneralText("You won the game!");
        }
    }

    
}