using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterConfig", menuName = "Character Config")]
public class CharacterConfigSO : ScriptableObject
{
    [Header("Character Stats")]
    public float speed; // Movement speed
    public float attackRange; // Attack range
}