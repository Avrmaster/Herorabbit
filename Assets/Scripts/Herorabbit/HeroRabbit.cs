using System;
using Commons;
using UnityEngine;
using World;

namespace Herorabbit
{
    public class HeroRabbit : MovingObject
    {
        public bool Idle = false;
        public float Speed = 3;
        public float MaxJumpTime = 2;
        public float JumpSpeed = 6.66f;
        public float GrewScaleFactor = 1.5f;

        public static HeroRabbit LastRabbit = null;
        private Vector3 _defaultScale;

        private bool _isGrewUp;
        private bool _jumpActive;
        private float _jumpTime;

        void Awake()
        {
            LastRabbit = this;
        }

        private new void Start()
        {
            base.Start();
            _defaultScale = transform.localScale;
        }

        private new void FixedUpdate()
        {
            base.FixedUpdate();
            if (IsDead || Idle)
                return;

            var isOnGround = IsOnGround();
            var value = Input.GetAxis("Horizontal");
            var velocity = Physics.velocity;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            var running = value != 0;

            if (running)
            {
                Sprite.flipX = value < 0;
            }

            if ((Mathf.Abs(value) > 0))
            {
                velocity.x = value * Speed;
            }

            if (Input.GetButtonDown("Jump") && isOnGround)
            {
                _jumpActive = true;
            }

            if (_jumpActive)
            {
                if (Input.GetButton("Jump"))
                {
                    _jumpTime += Time.deltaTime;
                    if (_jumpTime < MaxJumpTime)
                        velocity.y = JumpSpeed * (1.0f - _jumpTime / MaxJumpTime);
                }
                else
                {
                    _jumpActive = false;
                    _jumpTime = 0;
                }
            }

            Animator.SetBool("run", running);
            Physics.velocity = velocity;
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                _isGrewUp ? _defaultScale * GrewScaleFactor : _defaultScale, Time.deltaTime);
        }

        public void GrowUp()
        {
            _isGrewUp = true;
        }

        public void GrowDown()
        {
            _isGrewUp = false;
        }

        public bool IsGrewUp()
        {
            return _isGrewUp;
        }

        public void SmallJump()
        {
            var velocity = Physics.velocity;
            velocity.y = 2 * JumpSpeed / 3;
            Physics.velocity = velocity;
        }
    }
}