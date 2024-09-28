using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Weapon
{
    public class Weapon : MonoBehaviour
    {
        public Character.Character owner { get; private set; }
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private Bullet bullet;
        [SerializeField] private WeaponConfigSO weaponConfig;
        
        private float speed = 10f;
        private float rotationSpeed = 100f;
        public float reloadTime
        {
            get;
            private set;
        }
        private PoolType poolType;
        private BulletType bulletType;
        
        public void OnInit(Character.Character owner)
        {
            this.owner = owner;
            speed = weaponConfig.speed;
            rotationSpeed = weaponConfig.rotationSpeed;
            reloadTime = weaponConfig.reloadTime;
            poolType = weaponConfig.poolType;
            bulletType = weaponConfig.bulletType;
        }

        public void Fire(Vector3 direction)
        {
            Bullet currentBullet = SimplePool.Spawn<Bullet>(poolType, spawnPoint.position, Quaternion.identity);
            currentBullet.OnInit(this ,bulletType, owner.Range, speed, rotationSpeed, direction);
        }
    }
}

