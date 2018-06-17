using Commons;
using UnityEngine;

namespace World.Enemies
{
    public class Orc : MovingObject
    {
        public float WalkSpeed = 1.3f;
        public float RunSpeed = 3;

        public enum Mode
        {
            GoToA,
            GoToB,
            Attack
        }

        Mode mode = Mode.GoToA;
        
        private new void FixedUpdate()
        {
            base.FixedUpdate();
            
//            var value = Input.GetAxis("Horizontal");
//            var velocity = _orcPhysics.velocity;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
//            var running = value != 0;

//            if (running)
//            {
//                _sprite.flipX = value > 0;
//            }

//            if ((Mathf.Abs(value) > 0))
//            {
//                velocity.x = value * Speed;
//            }

//            if (Input.GetButtonDown("Jump") && isOnGround)
//            {
//                _jumpActive = true;
//            }

//            if (_jumpActive)
//            {
//                if (Input.GetButton("Jump"))
//                {
//                    _jumpTime += Time.deltaTime;
//                    if (_jumpTime < MaxJumpTime)
//                        velocity.y = JumpSpeed * (1.0f - _jumpTime / MaxJumpTime);
//                }
//                else
//                {
//                    _jumpActive = false;
//                    _jumpTime = 0;
//                }
//            }

//            _animator.SetBool("run", running);
//            _animator.SetBool("jump", !isOnGround);
//            _orcPhysics.velocity = velocity;
//            _orcPhysics.angularVelocity = 0;
//            _orcPhysics.rotation = 0;
        }
    }
}