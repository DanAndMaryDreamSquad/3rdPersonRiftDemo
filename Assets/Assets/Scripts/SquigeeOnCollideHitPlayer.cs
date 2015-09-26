using UnityEngine;
using System.Collections;

public class SquigeeOnCollideHitPlayer : MonoBehaviour {

	GameObject player;
	PlayerHealth playerHealth;
	bool isDefeated;

	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			playerHealth.Knocked(this.gameObject);
		}
	}
}
