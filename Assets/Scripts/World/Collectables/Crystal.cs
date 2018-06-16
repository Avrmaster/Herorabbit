using Herorabbit;

namespace World.Collectables
{
    public class Crystal : Collectable
    {
        protected override void OnRabitHit(HeroRabbit rabbit)
        {
            base.OnRabitHit(rabbit);
            LevelController.Current.OnCollectCrystal();
        }
    }
}