using UnityEngine;
using World;

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
        private Transform _defaultParent;

        private int _groundLayerId;
        private bool _jumpActive;
        private float _jumpTime;

        private void Start()
        {
            _groundLayerId = 1 << LayerMask.NameToLayer("Ground");
            _animator = GetComponentInChildren<Animator>();
            _sprite = GetComponentInChildren<SpriteRenderer>();
            _rabbitPhysics = GetComponent<Rigidbody2D>();
            _startingPosition = _rabbitPhysics.transform.position;
            _defaultParent = transform.parent;
        }

        private void FixedUpdate()
        {
            var hit = GetHit();
            var isOnGround = (bool) hit;
            SetupRelativeTransform(hit);

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
            _rabbitPhysics.rotation = 0;
        }

        public void Respawn()
        {
            _rabbitPhysics.transform.position = _startingPosition;
            _rabbitPhysics.MoveRotation(0);
        }

        private RaycastHit2D GetHit()
        {
            return Physics2D.Linecast(
                transform.position + Vector3.up * 0.1f,
                transform.position + Vector3.down * 0.1f,
                _groundLayerId);
        }

        private static void SetNewParent(Transform obj, Transform newParent)
        {
            if (obj.transform.parent != newParent)
            {
                var pos = obj.transform.position;
                obj.transform.parent = newParent;
                obj.transform.position = pos;
            }
        }

        private void SetupRelativeTransform(RaycastHit2D hit)
        {
            if (hit && hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
            {
                SetNewParent(transform, hit.transform);
            }
            else
            {
                SetNewParent(transform, _defaultParent);
            }
        }
    }
}