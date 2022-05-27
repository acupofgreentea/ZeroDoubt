using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public enum CharacterTypes
{
    Player,
    Enemy
}

public abstract class Character : MonoBehaviour
{
    [field: SerializeField] public CharacterTypes CharacterTypes { get; set; }
    [field: SerializeField] public BattleSystem BattleSystem { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public int HealAmount { get; set; }



    [Header("References")] 
    [SerializeField] protected TextMeshProUGUI characterNameText;

    [SerializeField] protected TextMeshProUGUI characterLevelText;

    [SerializeField] private TextMeshProUGUI attackDamageText;

    [SerializeField] private TextMeshProUGUI healAmountText;

    [SerializeField] protected TextMeshProUGUI healthBarText;
    
    [SerializeField] public TextMeshProUGUI abilityText;

    [SerializeField] protected Image healthBar;

    [SerializeField] private CharacterSO characterSO;



    public Character CurrentEnemy { get; set; }
    public string CharacterName { get; private set; }
    public int CurrentHp { get; set; }
    public int MaxHp { get; set; }
    public bool TurnCompleted { get; set; } = false;


    protected int characterLevel;
    
    private SpriteRenderer _spriteRenderer;


    private void OnEnable()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        CharacterName = characterSO.CharacterName;
        characterLevel = characterSO.CharacterLevel;
        _spriteRenderer.sprite = characterSO.Sprite;
        
        CurrentHp = characterSO.CurrentHp;
        MaxHp = characterSO.MaxHp;
    }

    private void OnDisable()
    {
        TurnCompleted = false;
    }

    protected virtual void Start()
    {
        SetHealthBar();
        characterNameText.text = CharacterName;
        characterLevelText.text = "Lvl." + characterLevel.ToString();

        abilityText.enabled = false;

        UpdateStatsTexts();
    }

    public void UpdateStatsTexts()
    {
        attackDamageText.text = Damage.ToString();
        healAmountText.text = HealAmount.ToString();
    }

    public void UpdateAbilityText(string message)
    {
        abilityText.enabled = true;
        abilityText.text = message;
        abilityText.transform.DOMoveY(abilityText.transform.position.y + 0.5f, 0.8f).OnComplete(()=> abilityText.enabled = false);
    }

    public bool TakeDamage(int dmg)
    {
        CurrentHp -= dmg;

        SetHealthBar();

        if (CurrentHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetHealthBar()
    {
        healthBar.fillAmount = (float)CurrentHp / MaxHp;

        if (CurrentHp <= 0) CurrentHp = 0;
        healthBarText.text = CurrentHp + " / " + MaxHp;
    }

    public abstract IEnumerator TurnChangeRoutine();
}