
using UnityEngine;

namespace _Game.Scripts.Gameplay.Character
{
    public class Player : Character
    {
        [SerializeField] private Joystick joystick;

        protected override void MoveDirection()
        {
            if (joystick.Direction == Vector2.zero || joystick == null)
            {
                direction = Vector3.zero;
                ChangeAnim("idle");
                return;
            }

            direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
            ChangeAnim("run");
            
            base.MoveDirection();
        }

        protected override void RotateDirection()
        {
            if (joystick.Direction == Vector2.zero || joystick == null)
            {
                direction = Vector3.zero;
                return;
            }

            base.RotateDirection();
        }

        protected override void SelectClosestTarget()
        {
            Character previousTarget = currentTarget;
            base.SelectClosestTarget();
            if (previousTarget != null)
                ((Enemy)previousTarget).NotTargetedByPlayer();
            ((Enemy)currentTarget).TargetedByPlayer();
        }

        public override void RemoveTargetInRange(Character character)
        {
            if (currentTarget == character)
                ((Enemy)currentTarget).NotTargetedByPlayer();
            base.RemoveTargetInRange(character);
        }
    }
}