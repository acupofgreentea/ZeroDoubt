using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected string characterName;
    [SerializeField] protected int characterLevel;
    [SerializeField] protected int currentHP;
    [SerializeField] protected int maxHP;

    protected virtual void Start()
    {
        currentHP = maxHP;
    }
}
