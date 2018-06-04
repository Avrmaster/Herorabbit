using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D collided) {
		var rabit = collided.GetComponent<HeroRabbit> ();
		if(rabit != null) {
			LevelController.Current.OnRabitDeath(rabit);
		}
	}
	
}
