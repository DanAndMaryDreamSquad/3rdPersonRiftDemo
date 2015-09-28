using UnityEngine;
using System.Collections;

public class SquigeeOnCollideHitPlayer : MonoBehaviour {

	GameObject player;
	PlayerHealth playerHealth;
	SquigeeMover squigeeMover;
	bool isDefeated;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		squigeeMover = GetComponent<SquigeeMover>();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			playerHealth.Knocked(this.gameObject);
			squigeeMover.KnockedPlayer();
		}
	}
}
