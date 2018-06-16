using Herorabbit;

namespace World.Collectables
{
    public class Coin : Collectable
    {
        protected override void OnRabitHit(HeroRabbit rabit)
        {
            LevelController.Current.OnCollectCoin();
            CollectedHide();
        }
    }
}