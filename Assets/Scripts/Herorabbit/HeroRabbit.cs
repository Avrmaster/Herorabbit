using System;
using Commons;
using UnityEngine;
using UnityEngine.SceneManagement;
using World;

namespace Herorabbit
{
    public class HeroRabbit : MovingObject
    {
        public bool Idle;
        public float Speed = 3;
        public float MaxJumpTime = 2;
        public float JumpSpeed = 6.66f;
        public float GrewScaleFactor = 1.5f;

        public AudioClip OnJumpedAudioClip;
        private AudioSource _onJumpedAudioSource;

        public static HeroRabbit LastRabbit;
        private Vector3 _defaultScale;

        private bool _isGrewUp;
        private bool _jumpActive;
        private float _jumpTime;
        private bool _lastOnGround;

        void Awake()
        {
            LastRabbit = this;
            _onJumpedAudioSource = gameObject.CreateAudioSource(OnJumpedAudioClip);
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
            if (isOnGround && !_lastOnGround)
            {
                Debug.Log("playing jump " + Time.time);
                _onJumpedAudioSource.volume = 1;
                _onJumpedAudioSource.PlayWithPrefs();
            }

            _lastOnGround = isOnGround;

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

            MoveAnimator.SetBool("run", running);
            Physics.velocity = velocity;
            transform.localScale = Vector3.Lerp(
                transform.localScale,
                _isGrewUp ? _defaultScale * GrewScaleFactor : _defaultScale, Time.deltaTime);
        }

        public new void OnDied()
        {
            base.OnDied();
            if (!IsToRespawn)
                SceneManager.LoadScene("LevelSelector");
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