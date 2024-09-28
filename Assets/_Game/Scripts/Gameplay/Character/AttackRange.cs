using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Character
{
    public class AttackRange : MonoBehaviour
    {
        [SerializeField] protected Character owner;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Ultilities.ENEMY) || other.CompareTag(Ultilities.PLAYER))
            {
                Character character = Ultilities.GetCharacter(other);
                owner.AddTargetInRange(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(Ultilities.ENEMY) || other.CompareTag(Ultilities.PLAYER))
            {
                Character character = Ultilities.GetCharacter(other);
                
                owner.RemoveTargetInRange(character);
            }
        }
    }
}
