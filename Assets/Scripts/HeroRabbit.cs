using System;
using UnityEngine;

public class HeroRabbit : MonoBehaviour
{
    public float Speed = 2;
    public float JumpSpeed = 6.66f;

    private Animator _animator;
    private Rigidbody2D _rabbit;
    private SpriteRenderer _sprite;

    private Vector3 _startingPosition;


    // Use this for initialization
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rabbit = GetComponent<Rigidbody2D>();
        _sprite = GetComponent<SpriteRenderer>();
        SetStartPosition(_rabbit.transform.position);
    }

    private void FixedUpdate()
    {
        var value = Input.GetAxis("Horizontal");
        // ReSharper disable once CompareOfFloatsByEqualityOperator
        var running = value != 0;

        if (running)
            _sprite.flipX = value < 0;
        _animator.SetBool("run", running);

        var velocity = _rabbit.velocity;
        if ((Mathf.Abs(value) > 0))
        {
            velocity.x = value * Speed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = JumpSpeed;
        }

        _animator.SetBool("jump", Math.Abs(velocity.y) > 0.2);

        _rabbit.velocity = velocity;
    }

    public void SetStartPosition(Vector3 pos)
    {
        this._startingPosition = pos;
    }

    public void OnRabitDeath()
    {
        _rabbit.transform.position = this._startingPosition;
        _rabbit.angularVelocity = 0;
        _rabbit.MoveRotation(0);
    }
}