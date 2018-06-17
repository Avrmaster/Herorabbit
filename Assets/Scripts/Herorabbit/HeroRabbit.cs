using System;
using Commons;
using UnityEngine;
using World;

namespace Herorabbit
{
    public class HeroRabbit : MovingObject
    {
        public float Speed = 3;
        public float MaxJumpTime = 2;
        public float JumpSpeed = 6.66f;

        public static HeroRabbit LastRabit = null;
        private Vector3 _defaultScale;

        private bool _jumpActive;
        private float _jumpTime;

        void Awake()
        {
            LastRabit = this;
        }

        private new void Start()
        {
            base.Start();
            _defaultScale = transform.localScale;
        }

        private new void FixedUpdate()
        {
            base.FixedUpdate();
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
        }

        public void GrowUp()
        {
            if (transform.localScale == _defaultScale)
            {
                transform.localScale = _defaultScale * 1.5f;
            }
        }

        public void GrowDown()
        {
            transform.localScale = _defaultScale;
        }

        public bool IsGrewUp()
        {
            return transform.localScale != _defaultScale;
        }
    }
}