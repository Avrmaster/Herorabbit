using Herorabbit;
using UnityEngine;

namespace World
{
	public class DeathZone : MonoBehaviour {

		void OnTriggerEnter2D(Collider2D collided) {
			var rabit = collided.GetComponent<HeroRabbit> ();
			if(rabit != null) {
				LevelController.Current.OnRabbitDeath(rabit);
			}
		}
	
	}
}
