using UnityEngine;

namespace _Game.Scripts.Gameplay.Character
{
    public class Enemy : Character
    {
        [SerializeField] private GameObject indicator;

        public void TargetedByPlayer()
        {
            indicator.SetActive(true);
        }
        
        public void NotTargetedByPlayer()
        {
            indicator.SetActive(false);
        }
    }
}
