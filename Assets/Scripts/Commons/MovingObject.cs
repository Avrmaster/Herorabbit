using UnityEngine;
using UnityEngine.SceneManagement;
using World;

namespace Commons
{
    public abstract class MovingObject : MonoBehaviour
    {
        public AudioClip OnDiedAudioClip;

        private Vector3 _startingPosition;
        private Transform _defaultParent;
        private int _groundLayerId;
        private bool _lastOnGround;
        private AudioSource _onDiedAudioSource;

        protected SpriteRenderer Sprite;
        protected Rigidbody2D Physics;
        protected Animator MoveAnimator;
        public bool IsDead;

        protected void Start()
        {
            Sprite = GetComponentInChildren<SpriteRenderer>();
            MoveAnimator = GetComponentInChildren<Animator>();
            Physics = GetComponent<Rigidbody2D>();

            _groundLayerId = 1 << LayerMask.NameToLayer("Ground");
            _startingPosition = Physics.transform.position;
            _defaultParent = transform.parent;
            _onDiedAudioSource = gameObject.CreateAudioSource(OnDiedAudioClip);
        }

        protected void FixedUpdate()
        {
            var hit = GetHit();
            _lastOnGround = hit;
            SetupRelativeTransform(hit);

            MoveAnimator.SetBool("jump", !_lastOnGround);
            Physics.angularVelocity = 0;
            Physics.rotation = 0;
        }

        protected bool IsOnGround()
        {
            return _lastOnGround;
        }

        private RaycastHit2D GetHit()
        {
            var hitStart = transform.position + Vector3.up * 0.4f;
            var hitEnd = transform.position + Vector3.down * 0.1f;
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

        protected bool IsToRespawn;

        public void Kill(bool withRespawn)
        {
            if (IsDead) return;
            _onDiedAudioSource.PlayWithPrefs();
            IsDead = true;
            MoveAnimator.SetTrigger("die");
            IsToRespawn = withRespawn;
        }

        public void OnDied()
        {
            if (IsToRespawn)
                Respawn();
        }

        private void Respawn()
        {
            IsDead = false;
            MoveAnimator.SetTrigger("respawn");
            Physics.transform.position = _startingPosition;
            Physics.MoveRotation(0);
        }

        public static bool HasArrived(Vector3 pos, Vector3 start, Vector3 target)
        {
            return Vector3.Distance(target, start) < Vector3.Distance(pos, start);
        }
    }
}