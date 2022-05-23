using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Character", menuName = "Character", order = 0)]
public class CharacterSO : ScriptableObject
{
    [field: SerializeField] public string CharacterName { get; set; }
    [field: SerializeField] public int CharacterLevel { get; set; } 
    [field: SerializeField] public Sprite Sprite { get; set; }
    
    
    [field: SerializeField] public int HealAmount { get; set; }
    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public int CurrentHp { get; set;}
    [field: SerializeField] public int MaxHp { get; set; }

    private void OnEnable()
    {
        CurrentHp = MaxHp;
    }
}