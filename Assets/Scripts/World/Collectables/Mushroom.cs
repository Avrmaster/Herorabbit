using Herorabbit;

namespace World.Collectables
{
    public class Mushroom : Collectable
    {
        protected override void OnRabitHit(HeroRabbit rabbit)
        {
            base.OnRabitHit(rabbit);
            rabbit.GrowUp();
        }
    }
}