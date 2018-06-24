using Herorabbit;
using UnityEngine.SceneManagement;

namespace World.Collectables
{
    public class LevelEntry : Collectable
    {
        public int LevelNumber = 1;

        protected override void OnRabitHit(HeroRabbit rabbit)
        {
            SceneManager.LoadScene(string.Format("Level{0}", LevelNumber));
        }
    }
}