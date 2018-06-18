using System;
using Commons;
using Herorabbit;
using UnityEngine;

namespace World.Enemies
{
    public abstract class Orc : MovingObject
    {
        public float WalkSpeed = 1.3f;
        public float RunSpeed = 2;

        public float WaitTime = 3;
        public float PatrolDistance = 2;

        private enum Mode
        {
            GoToA,
            GoToB,
            Attack
        }

        private Mode _mode = Mode.GoToA;
        private float _waitTimeout = 0;
        private bool _dying = false;
        protected Vector3 PointA;
        protected Vector3 PointB;

        private new void Start()
        {
            base.Start();
            PointA = transform.position;
            PointB = transform.position + Vector3.right * PatrolDistance;
        }

        private new void FixedUpdate()
        {
            base.FixedUpdate();
            if (IsDead || _dying)
                return;

            PointA.y = PointB.y = transform.position.y;

            Debug.DrawLine(PointA, PointB, Color.cyan);

            if (HasToAttack())
                _mode = Mode.Attack;
            else if (_mode == Mode.Attack)
                _mode = Mode.GoToA;

            if (_mode == Mode.Attack)
            {
                Atack();
            }
            else
            {
                StopAtack();
                if (_waitTimeout > 0)
                    _waitTimeout -= Time.deltaTime;
                else
                {
                    var myPos = transform.position;
                    var start = _mode == Mode.GoToA ? PointB : PointA;
                    var target = _mode == Mode.GoToA ? PointA : PointB;

                    if (HasArrived(myPos, start, target))
                    {
                        _waitTimeout = WaitTime;
                        _mode = _mode == Mode.GoToA ? Mode.GoToB : Mode.GoToA;
                    }

                    var curVelocity = Physics.velocity;
                    curVelocity.x = WalkSpeed * (_mode == Mode.GoToA ? -1 : 1);
                    Physics.velocity = curVelocity;
                }
            }

            var moving = Math.Abs(Physics.velocity.x) > 0.1f;

            if (moving)
                Sprite.flipX = Physics.velocity.x > 0;

            Animator.SetBool("walk", moving);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            {
                var rabbit = col.gameObject.GetComponent<HeroRabbit>();
                if (rabbit != null && !_dying)
                {
                    var angle = Math.Atan2(
                        rabbit.transform.position.y - transform.position.y,
                        rabbit.transform.position.x - transform.position.x
                    );

                    if (angle > Math.PI / 4 && angle < 3 * Math.PI / 4)
                    {
                        _dying = true;
                        Animator.SetTrigger("die");
                    }
                    else
                    {
                        Animator.SetBool("atack", true);
                        LevelController.Current.OnRabbitDeath(rabbit);
                    }
                }
            }
        }

        public new void OnDied()
        {
            Destroy(gameObject);
        }

        protected abstract bool HasToAttack();

        protected abstract void Atack();

        protected abstract void StopAtack();
    }
}