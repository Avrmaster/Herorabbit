using Herorabbit;
using JetBrains.Annotations;
using UnityEngine;

namespace World.Enemies
{
    public class GreenOrc : Orc
    {
        protected override bool HasToAttack()
        {
            if (HeroRabbit.LastRabbit == null)
                return false;

            var rabbit = HeroRabbit.LastRabbit;
            var rabbitX = rabbit.transform.position.x;

            return rabbitX > Mathf.Min(PointA.x, PointB.x)
                   && rabbitX < Mathf.Max(PointA.x, PointB.x);
        }

        protected override void Atack()
        {
            var rabbit = HeroRabbit.LastRabbit;
            var rabbitX = rabbit.transform.position.x;

            var curVelocity = Physics.velocity;
            curVelocity.x = RunSpeed * (transform.position.x > rabbitX ? -1 : 1);
            Physics.velocity = curVelocity;
        }
    }
}