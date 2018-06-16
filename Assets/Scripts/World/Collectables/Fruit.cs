using Herorabbit;

namespace World.Collectables
{
    public class Fruit : Collectable
    {
        protected override void OnRabitHit(HeroRabbit rabbit)
        {
            base.OnRabitHit(rabbit);
            LevelController.Current.OnCollectFruit();
        }
    }
}