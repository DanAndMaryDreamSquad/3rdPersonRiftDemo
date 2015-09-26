using UnityEngine;
using System.Collections;

public class SquigeeMover : MonoBehaviour {

	Transform player;
	PlayerMover playerMover;
	Animator animator;
	NavMeshAgent nav;
	bool isWalking = false;
	SquigeeHealth squigeeHealth;

	void Awake() {
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		player = p.transform;
		animator = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
		squigeeHealth = GetComponent<SquigeeHealth>();
	}

	void Update() {
		if (!squigeeHealth.isDefeated) {
			nav.SetDestination(player.position);
			animator.SetBool("IsWalking", !ReachedDestination());
		} else {
			nav.enabled = false;
		}
	}

	bool ReachedDestination() {
		if (!nav.pathPending){
			if (nav.remainingDistance <= nav.stoppingDistance){
				if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f){
					return true;
				}
			}
		}
		return false;
	}

}
