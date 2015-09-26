using UnityEngine;
using System.Collections;

public class SquigeeOnCollideBounce : MonoBehaviour {

	GameObject player;
	PlayerHealth playerHealth;
	PlayerMover playerMover;
	GameObject squigee;
	SquigeeHealth squigeeHealth;
	
	void Awake() {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHealth = player.GetComponent<PlayerHealth> ();
		playerMover = player.GetComponent<PlayerMover> ();
		squigee = this.transform.parent.gameObject;
		squigeeHealth = squigee.GetComponent<SquigeeHealth>();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (player.transform.position.y < transform.position.y) {
				return;
			}
			squigeeHealth.BouncedOn();
			playerMover.Bounced();
		}
	}
}
