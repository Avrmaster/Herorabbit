using Herorabbit;

namespace World.Collectables
{
    public class Bomb : Collectable
    {
        protected override void OnRabitHit(HeroRabbit rabbit)
        {
            base.OnRabitHit(rabbit);
            LevelController.Current.OnRabbitDeath(rabbit);
        }
    }
}