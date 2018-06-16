using Herorabbit;
using UnityEngine;

namespace World
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Current;

        void Awake()
        {
            Current = this;
        }

        private int _score;

        public void OnRabitDeath(HeroRabbit heroRabbit)
        {
            heroRabbit.Respawn();
        }

        public void OnCollectCoin()
        {
            _score += 1;
        }
    }
}