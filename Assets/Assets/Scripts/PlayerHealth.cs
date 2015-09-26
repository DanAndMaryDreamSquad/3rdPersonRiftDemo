using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	Animator animator;
	PlayerMover playerMover;

	void Awake () {
		animator = GetComponent<Animator> ();
		playerMover = GetComponent<PlayerMover>();
	}

	public void Knocked(GameObject knocker) {
		if (!playerMover.isBeingKnocked) {
			animator.SetTrigger("IsKnockedDown");
			//playerMover.Knocked(knocker);
		}
	}
}
