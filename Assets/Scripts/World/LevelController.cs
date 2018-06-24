using Herorabbit;
using UnityEngine;
using UnityEngine.UI;

namespace World
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Current;
        public int LifesCount = 3;
        public int MaxFruitsCount = 11;
        public Text CoinsText;
        public Text FruitText;
        public Image LifesHolder;
        public Image CrystalsHolder;

        public GameObject LifePrefab;
        public GameObject LifeEmptyPrefab;
        public GameObject CrystalPrefab;
        public GameObject CrystalEmptyPrefab;

        private int _lifesLeft;
        private int _coinsCollected;
        private int _fruitCollected;
        private int _crystalsCollected;

        void Awake()
        {
            Current = this;
            _lifesLeft = LifesCount;
            UpdateContainers();
        }


        public void OnRabbitDeath(HeroRabbit heroRabbit)
        {
            if (heroRabbit.IsGrewUp())
            {
                heroRabbit.GrowDown();
            }
            else
            {
                --_lifesLeft;
                heroRabbit.Kill(LifesCount == -1 || _lifesLeft > 0);
            }

            UpdateContainers();
        }

        public void OnCollectCoin()
        {
            _coinsCollected += 1;
            UpdateContainers();
        }

        public void OnCollectFruit()
        {
            _fruitCollected += 1;
            UpdateContainers();
        }

        public void OnCollectCrystal()
        {
            _crystalsCollected += 1;
            UpdateContainers();
        }

        private void UpdateContainers()
        {
            if (CoinsText)
                CoinsText.text = _coinsCollected.ToFixedLengthString(4);
            if (FruitText)
                FruitText.text = _fruitCollected.ToProportion(MaxFruitsCount);
            FillContainer(LifesHolder, LifePrefab, LifeEmptyPrefab, _lifesLeft, LifesCount);
            FillContainer(CrystalsHolder, CrystalPrefab, CrystalEmptyPrefab, _crystalsCollected);
        }

        private void FillContainer(Image holder, GameObject prefab, GameObject emptyPrefab,
            int count, int maxCount = 3)
        {
            if (!holder || !prefab || !emptyPrefab)
                return;

            foreach (Transform child in holder.transform)
            {
                Destroy(child.gameObject);
            }

            for (int i = 0; i < maxCount; i++)
            {
                var newObj = Instantiate(i < count ? prefab : emptyPrefab);
                newObj.transform.SetParent(holder.transform);
                var width = ((RectTransform) newObj.transform).rect.width;
                var pos = holder.transform.position;
                pos.x += width * (0.5f + i - ((float) maxCount / 2));
                newObj.transform.position = pos;
            }
        }


        public void OnPausePressed()
        {
            
        }

    }
}