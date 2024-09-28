using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Game.Scripts.Gameplay.Character
{
    public class Character : MonoBehaviour
    {
        public event Action OnCharacterDeath;
        
        [SerializeField] protected Rigidbody rb;
        [SerializeField] protected Animator animator;
        [SerializeField] protected Transform model;
        [SerializeField] protected Transform weaponHolder;
        [SerializeField] protected Weapon.Weapon currentWeapon;
        [SerializeField] protected CharacterConfigSO config;
        [SerializeField] private AttackRange attackRange;

        private float speed;

        public float Range { get; private set; }

        private List<Character> targets;
        protected Character currentTarget;
        private Dictionary<Character, Action> deathHandlers;
        protected Vector3 direction;
        private string currentAnimName;
        private bool canAttack = true;
        
        protected bool isMoving
        {
            get { return direction != Vector3.zero; }
        }

        private void Start()
        {
            OnInit();
        }

        private void Update()
        {
            if (targets.Count <= 0)
                return;
            SelectClosestTarget();
            
            Attack();
        }

        protected void FixedUpdate()
        {
            MoveDirection();
            RotateDirection();
        }

        public void OnInit()
        {
            direction = Vector3.zero;
            targets = new List<Character>();
            deathHandlers = new Dictionary<Character, Action>();
            speed = config.speed;
            Range = config.attackRange * 1.2f;
            
            if (attackRange != null)
                attackRange.transform.localScale = new Vector3(config.attackRange, config.attackRange, config.attackRange);
            if (currentWeapon != null)
                currentWeapon.OnInit(this);
            ChangeAnim("idle");
        }

        protected virtual void MoveDirection()
        {
            rb.AddForce(direction * speed);
        }

        protected virtual void RotateDirection()
        {
            model.LookAt(transform.position + direction);
        }

        protected virtual void Attack()
        {
            if (currentWeapon == null)
                return;
            
            if (isMoving)
                return;
            
            if (targets.Count <= 0)
                return;
            
            if (!canAttack)
                return;

            Vector3 shootDirection = (currentTarget.transform.position - transform.position).normalized;
            currentWeapon.Fire(shootDirection);
            model.LookAt(transform.position + shootDirection);
            ChangeAnim("attack");
            
            StartCoroutine(Reload());
        }
        
        private IEnumerator Reload()
        {
            canAttack = false; // Set the flag to false, preventing further attacks

            // Wait for the reload time
            yield return new WaitForSeconds(currentWeapon.reloadTime);

            canAttack = true; // Set the flag back to true, allowing attacks
        }
    
        protected void ChangeAnim(string animName)
        {
            if (animator == null)
                return;
        
            if (currentAnimName != animName)
            {
                animator.ResetTrigger(currentAnimName);
                currentAnimName = animName;
                animator.SetTrigger(currentAnimName);
            }
        }

        public void AddTargetInRange(Character character)
        {
            if (character != null)
                targets.Add(character);
            Action handler = () => RemoveTargetInRange(character); // Store the handler
            deathHandlers[character] = handler; // Store the handler for unsubscribing
            character.OnCharacterDeath += handler; // Subscribe
        }

        public virtual void RemoveTargetInRange(Character character)
        {
            targets.Remove(character);

            if (currentTarget == character)
                currentTarget = null;
            character.OnCharacterDeath -= deathHandlers[character]; // Unsubscribe using the stored handler
            deathHandlers.Remove(character); // Clean up the dictionary
        }

        protected virtual void SelectClosestTarget()
        {
            var sortedCharacters = targets
                .OrderBy(character => Vector3.Distance(character.transform.position, transform.position))
                .ToList();
            currentTarget = sortedCharacters[0];
        }

        public virtual void Die()
        {
            Debug.Log(gameObject.name + "Die");
            gameObject.SetActive(false);
            OnCharacterDeath?.Invoke();
        }
    }
}
