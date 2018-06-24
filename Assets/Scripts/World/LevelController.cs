using Herorabbit;
using UnityEngine;
using UnityEngine.UI;

namespace World
{
    [System.Serializable]
    class LevelStat
    {
        public int CoinsCollected;

        internal static LevelStat Load()
        {
            return JsonUtility.FromJson<LevelStat>(PlayerPrefs.GetString("stats", null)) ?? new LevelStat();
        }

        internal void Save()
        {
            PlayerPrefs.SetString("stats", JsonUtility.ToJson(this));
            PlayerPrefs.Save();
        }
    }

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

        public AudioClip BackgroundMusiClip;
        private AudioSource _musicSource;

        private int _lifesLeft;
        private int _fruitCollected;
        private int _crystalsCollected;

        private LevelStat _levelStat;

        void Awake()
        {
            Current = this;
            _lifesLeft = LifesCount;
            _levelStat = LevelStat.Load();
            UpdateContainers();

            _musicSource = gameObject.CreateAudioSource(BackgroundMusiClip);
            _musicSource.volume = 0.4f;
            _musicSource.loop = true;
            _musicSource.Play();
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
            _levelStat.CoinsCollected += 1;
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
                CoinsText.text = _levelStat.CoinsCollected.ToFixedLengthString(4);
            if (FruitText)
                FruitText.text = _fruitCollected.ToProportion(MaxFruitsCount);
            FillContainer(LifesHolder, LifePrefab, LifeEmptyPrefab, _lifesLeft, LifesCount);
            FillContainer(CrystalsHolder, CrystalPrefab, CrystalEmptyPrefab, _crystalsCollected);
            _levelStat.Save();
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

            for (var i = 0; i < maxCount; i++)
            {
                var newObj = Instantiate(i < count ? prefab : emptyPrefab);
                newObj.transform.SetParent(holder.transform);

                var scl = 1f / newObj.transform.localScale.x;
                newObj.transform.localScale = new Vector3(1, 1, 1);

                var image = newObj.GetComponent<Image>();
                var width = image.rectTransform.rect.width;
                var pos = holder.rectTransform.position;

                pos.x += 1.2f * width * scl * (0.5f + i - ((float) maxCount / 2));
                image.rectTransform.position = pos;
            }
        }


        public void OnPausePressed()
        {
        }
    }
}