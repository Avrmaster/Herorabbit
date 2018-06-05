using UnityEngine;

namespace Herorabbit
{
    public class HeroRabbit : MonoBehaviour
    {
        public float Speed = 3;
        public float MaxJumpTime = 2;
        public float JumpSpeed = 6.66f;

        private Animator _animator;
        private Rigidbody2D _rabbitPhysics;
        private SpriteRenderer _sprite;

        private Vector3 _startingPosition;

        private int _groundLayerId;
        private bool _jumpActive;
        private float _jumpTime;

        private void Start()
        {
            _groundLayerId = 1 << LayerMask.NameToLayer("Ground");
            _animator = GetComponentInChildren<Animator>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
            _rabbitPhysics = GetComponent<Rigidbody2D>();
            SetStartPosition(_rabbitPhysics.transform.position);
        }

        private void FixedUpdate()
        {
            var isOnGround = IsOnGround();
            var value = Input.GetAxis("Horizontal");
            var velocity = _rabbitPhysics.velocity;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            var running = value != 0;

            if (running)
            {
                _sprite.flipX = value < 0;
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

            _animator.SetBool("run", running);
            _animator.SetBool("jump", !isOnGround);
            _rabbitPhysics.velocity = velocity;
            _rabbitPhysics.angularVelocity = 0;
        }

        public void OnRabitDeath()
        {
            _rabbitPhysics.transform.position = _startingPosition;
            _rabbitPhysics.MoveRotation(0);
        }

        private void SetStartPosition(Vector3 pos)
        {
            _startingPosition = pos;
        }

        private bool IsOnGround()
        {
            return Physics2D.Linecast(
                transform.position,
                transform.position + Vector3.down * 0.2f,
                _groundLayerId);
        }
    }
}