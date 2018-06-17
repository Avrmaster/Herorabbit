using UnityEngine;
using World;

namespace Commons
{
    public abstract class MovingObject : MonoBehaviour
    {
        private Vector3 _startingPosition;
        private Transform _defaultParent;
        private int _groundLayerId;
        private bool _lastIsOnGround;

        protected SpriteRenderer Sprite;
        protected Rigidbody2D Physics;
        protected Animator Animator;

        protected void Start()
        {
            Sprite = GetComponentInChildren<SpriteRenderer>();
            Animator = GetComponentInChildren<Animator>();
            Physics = GetComponent<Rigidbody2D>();

            _groundLayerId = 1 << LayerMask.NameToLayer("Ground");
            _startingPosition = Physics.transform.position;
            _defaultParent = transform.parent;
        }

        protected void FixedUpdate()
        {
            var hit = GetHit();
            _lastIsOnGround = hit;
            SetupRelativeTransform(hit);

            Animator.SetBool("jump", !_lastIsOnGround);
            Physics.angularVelocity = 0;
            Physics.rotation = 0;
        }

        protected bool IsOnGround()
        {
            return _lastIsOnGround;
        }

        private RaycastHit2D GetHit()
        {
            var hitStart = transform.position + Vector3.up * 0.4f;
            var hitEnd = transform.position + Vector3.down * 0.1f;
            Debug.DrawLine(hitStart, hitEnd, Color.magenta);
            return Physics2D.Linecast(hitStart, hitEnd, _groundLayerId);
        }

        private static void SetNewParent(Transform obj, Transform newParent)
        {
            if (obj.transform.parent == newParent) return;
            var pos = obj.transform.position;
            obj.transform.parent = newParent;
            obj.transform.position = pos;
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

        private bool _isToRespawn;

        public void Kill(bool withRespawn)
        {
            Animator.SetTrigger("die");
            _isToRespawn = withRespawn;
        }

        public void OnDied()
        {
            if (_isToRespawn)
                Respawn();
        }

        private void Respawn()
        {
            Animator.SetTrigger("respawn");
            Physics.transform.position = _startingPosition;
            Physics.MoveRotation(0);
        }
    }
}