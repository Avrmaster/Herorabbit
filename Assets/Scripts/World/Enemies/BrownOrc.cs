using System.CodeDom.Compiler;
using Herorabbit;
using UnityEngine;
using World.Collectables;

namespace World.Enemies
{
    public class BrownOrc : Orc
    {
        public GameObject PrefabCarrot;
        public float TriggerDistance = 4f;
        public float RechargeTime = 3;
        private float _rechargeLeft = 0;

        protected override bool HasToAttack()
        {
            var rabbit = HeroRabbit.LastRabbit;
            if (rabbit)
            {
                return Vector3.Distance(
                           rabbit.transform.position,
                           transform.position
                       ) < TriggerDistance;
            }

            return false;
        }

        protected override void Atack()
        {
            if (_rechargeLeft > 0)
                _rechargeLeft -= Time.deltaTime;
            else
            {
                _rechargeLeft = RechargeTime;
                LaunchCarrot(HeroRabbit.LastRabbit.transform.position.x > transform.position.x);
            }
        }

        protected override void StopAtack()
        {
        }

        private void LaunchCarrot(bool direction)
        {
            var obj = Instantiate(PrefabCarrot);
            var position = transform.position;
            position.y += 0.5f;
            obj.transform.position = position;
            obj.GetComponent<Carrot>().Launch(direction);
            Sprite.flipX = direction;
        }
    }
}