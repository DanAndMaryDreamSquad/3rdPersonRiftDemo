using UnityEngine;
using System.Collections;

public class SquigeeMover : MonoBehaviour {

	public float aggroDistance;
	Transform player;
	PlayerMover playerMover;
	Animator animator;
	NavMeshAgent nav;
	bool isWalking = false;
	SquigeeHealth squigeeHealth;
	bool isAggroed = false;

	void Awake() {
		GameObject p = GameObject.FindGameObjectWithTag("Player");
		player = p.transform;
		playerMover = p.GetComponent<PlayerMover>();
		animator = GetComponent<Animator>();
		nav = GetComponent<NavMeshAgent>();
		squigeeHealth = GetComponent<SquigeeHealth>();
	}

	void Update() {
		CheckAggro();
		if (!squigeeHealth.isDefeated && isAggroed) {
			if (playerMover.isBeingKnocked) {
				nav.SetDestination(this.transform.position);
			} else {
				nav.SetDestination(player.position);
				animator.SetBool("IsWalking", !ReachedDestination());
			}
		} else {
			nav.enabled = false;
		}
	}

	void CheckAggro() {
		if (playerMover.isBeingKnocked) {
			isAggroed = false;
		}
		if (isAggroed) {
			return;
		}
		Vector3 distance = player.transform.position - this.transform.position;
		if (distance.sqrMagnitude < aggroDistance) {
			isAggroed = true;
			nav.enabled = true;
		}
	}

	bool ReachedDestination() {
		if (nav.pathStatus == NavMeshPathStatus.PathPartial) {
			return true;
		}
		if (!nav.pathPending){
			if (nav.remainingDistance <= nav.stoppingDistance){
				if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f){
					return true;
				}
			}
		}
		return false;
	}

	public void KnockedPlayer() {
		isAggroed = false;
		animator.SetBool("IsWalking", false);
	}

}
