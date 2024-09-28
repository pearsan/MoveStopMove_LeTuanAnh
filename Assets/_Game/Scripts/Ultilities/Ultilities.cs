using System.Collections;
using System.Collections.Generic;
using _Game.Scripts.Gameplay.Character;
using UnityEngine;

public static class Ultilities 
{
    public static string PLAYER = "Player";
    public static string ENEMY  = "Enemy";
    public static string BULLET = "Bullet";
    
    private static Dictionary<Collider, Character> characters  = new Dictionary<Collider, Character>();
    
    public static Character GetCharacter(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }

        return characters[collider];
    } 
}
