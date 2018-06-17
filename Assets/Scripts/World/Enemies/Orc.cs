using System.Runtime.Serialization.Formatters;
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
            if (IsDead)
                return;

            PointA.y = PointB.y = transform.position.y;

            Debug.DrawLine(PointA, PointB, Color.cyan);

            if (HasToAttack())
                _mode = Mode.Attack;
            else if (_mode == Mode.Attack)
                _mode = Mode.GoToA;

            var walking = false;
            var running = false;
            if (_mode == Mode.Attack)
            {
                running = true;
                Atack();
            }
            else
            {
                if (_waitTimeout > 0)
                    _waitTimeout -= Time.deltaTime;
                else
                {
                    walking = true;
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

            if (running || walking)
                Sprite.flipX = Physics.velocity.x > 0;

            Animator.SetBool("walk", walking);
            Animator.SetBool("run", _mode == Mode.Attack);
        }

        void OnCollisionEnter2D(Collision2D col)
        {
            {
                var rabbit = col.gameObject.GetComponent<HeroRabbit>();
                if (rabbit != null)
                {
                    Animator.SetBool("atack", true);
                    LevelController.Current.OnRabbitDeath(rabbit);
                }
            }
        }

        protected abstract bool HasToAttack();

        protected abstract void Atack();
    }
}