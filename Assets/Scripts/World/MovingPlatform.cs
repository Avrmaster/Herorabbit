using Commons;
using UnityEngine;

namespace World
{
    public class MovingPlatform : MonoBehaviour
    {
        public Vector3 MoveBy = Vector3.right * 5;
        public float WaitTime = 3;
        public float Speed = 2;

        private Vector3 _pointA;
        private Vector3 _pointB;
        private bool _movingForward = true;
        private float _waitTimeout = 0;

        void Start()
        {
            _pointA = transform.position;
            _pointB = _pointA + MoveBy;
            _waitTimeout = WaitTime;
        }

        void Update()
        {
            var myPos = transform.position;
            var start = _movingForward ? _pointA : _pointB;
            var target = _movingForward ? _pointB : _pointA;
            if (MovingObject.HasArrived(myPos, start, target))
            {
                _movingForward = !_movingForward;
                _waitTimeout = WaitTime;
            }

            if (_waitTimeout > 0)
                _waitTimeout -= Time.deltaTime;
            else
                transform.position = myPos + (target - start).normalized * Speed * Time.deltaTime;
        }
    }
}