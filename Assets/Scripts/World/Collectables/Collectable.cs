using Herorabbit;
using UnityEngine;

namespace World.Collectables
{
    public class Collectable : MonoBehaviour
    {
        protected virtual void OnRabitHit(HeroRabbit rabit)
        {
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
//            if (!this.hideAnimation)
            {
                var rabbit = collider.GetComponent<HeroRabbit>();
                if (rabbit != null)
                {
                    OnRabitHit(rabbit);
                }
            }
        }

        protected void CollectedHide()
        {
            Destroy(gameObject);
        }
    }
}