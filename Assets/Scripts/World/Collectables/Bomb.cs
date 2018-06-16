using Herorabbit;

namespace World.Collectables
{
    public class Bomb : Collectable
    {
        protected override void OnRabitHit(HeroRabbit rabbit)
        {
            base.OnRabitHit(rabbit);
            if (rabbit.IsGrewUp())
            {
                rabbit.GrowDown();
            }
            else
            {
                LevelController.Current.OnRabitDeath(rabbit);
            }
        }
    }
}