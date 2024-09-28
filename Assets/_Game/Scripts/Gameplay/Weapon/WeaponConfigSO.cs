using UnityEngine;

namespace _Game.Scripts.Gameplay.Weapon
{
    [CreateAssetMenu(fileName = "NewWeaponConfig", menuName = "Weapon Config")]
    public class WeaponConfigSO : ScriptableObject
    {
        public float speed = 10f;
        public float rotationSpeed = 100f;
        public float reloadTime = 2f;
        public PoolType poolType;
        public BulletType bulletType;
    }
    
    public enum BulletType
    {
        FlyStraight,
        RotateHorizontal,
        RotateVertical
    }
}
