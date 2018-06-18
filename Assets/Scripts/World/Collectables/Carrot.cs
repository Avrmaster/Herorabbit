using System.Collections;
using Herorabbit;
using UnityEngine;

namespace World.Collectables
{
    public class Carrot : Collectable
    {
        public float SelfDestroyTimeout = 3;
        public float FlySpeed = 2;
        private float _speed;

        void Start()
        {
            StartCoroutine(DestroyLater());
        }

        private IEnumerator DestroyLater()
        {
            yield return new WaitForSeconds(SelfDestroyTimeout);
            Destroy(gameObject);
        }

        protected override void OnRabitHit(HeroRabbit rabbit)
        {
            LevelController.Current.OnRabbitDeath(rabbit);
        }

        private void FixedUpdate()
        {
            var position = transform.position;
            position.x += Time.deltaTime * _speed;
            transform.position = position;
        }

        public void Launch(bool direction)
        {
            _speed = FlySpeed * (direction? 1 : -1);
        }
    }
}