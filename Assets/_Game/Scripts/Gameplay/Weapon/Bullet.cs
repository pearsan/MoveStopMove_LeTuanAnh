using System;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Weapon
{
    public class Bullet : GameUnit
    {
        private BulletType bulletType;
        private Transform spawnPoint;
        private float attackRange;
        private Weapon weapon;
        [SerializeField] private Transform model;
        private float speed = 10f;
        private float rotationSpeed = 100f;
        private Vector3 direction;

        public void OnInit(Weapon weapon, BulletType bulletType ,float range, float speed, float rotationSpeed,Vector3 direction)
        {
            spawnPoint = weapon.owner.transform;
            this.weapon = weapon;
            this.bulletType = bulletType;
            attackRange = range;
            this.speed = speed;
            this.rotationSpeed = rotationSpeed;
            this.direction = direction;
        }
        private void Update()
        {
            switch (bulletType)
            {
                case BulletType.FlyStraight:
                    break;

                case BulletType.RotateHorizontal:
                    model.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
                    break;

                case BulletType.RotateVertical:
                    model.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
                    break;
            }
            
            MoveBullet();
            
            if (Vector3.Distance(spawnPoint.position, transform.position) > attackRange)
            {
                SimplePool.Despawn(this);
            }
        }
        
        private void MoveBullet()
        {
            transform.Translate(direction * (speed * Time.deltaTime));
        }

        private void OnCollisionEnter(Collision collision)
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Ultilities.PLAYER) || other.gameObject.CompareTag(Ultilities.ENEMY))
            {
                Character.Character hit = Ultilities.GetCharacter(other);
                if (hit != weapon.owner)
                {
                    Debug.Log("HIT!");
                    hit.Die();
                    SimplePool.Despawn(this);
                }
            }
        }
    }
}
