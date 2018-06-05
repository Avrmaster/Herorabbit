using Herorabbit;
using UnityEngine;

namespace World
{
	public class LevelController : MonoBehaviour {

		public static LevelController Current;
		void Awake() {
			Current = this;
		}

		public void OnRabitDeath(HeroRabbit heroRabbit)
		{
			heroRabbit.OnRabitDeath();
		}
	
	}
}
