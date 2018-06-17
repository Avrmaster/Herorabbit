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

        private int _coinsCollected;
        private int _fruitCollected;
        private int _crystalsCollected;

        public void OnRabbitDeath(HeroRabbit heroRabbit)
        {
            if (heroRabbit.IsGrewUp())
            {
                heroRabbit.GrowDown();
            }
            else
            {
                heroRabbit.Kill(true);
            }
        }

        public void OnCollectCoin()
        {
            _coinsCollected += 1;
        }

        public void OnCollectFruit()
        {
            _fruitCollected += 1;
        }

        public void OnCollectCrystal()
        {
            _crystalsCollected += 1;
        }
    }
}