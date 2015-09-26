using UnityEngine;
using System.Collections;

public class SquigeeHealth : MonoBehaviour {

	public GameObject explosion;
	Animator animator;
	public bool isDefeated = false;

	void Awake () {
		animator = GetComponent <Animator> ();
	}

	public void BouncedOn() {
		animator.SetTrigger("BouncedOn");
		Defeated();
	}

	void Defeated () {
		isDefeated = true;
	}

	public void PoofAway () {
		// Find and disable the Nav Mesh Agent.
		GetComponent <NavMeshAgent> ().enabled = false;
		
		// After 0 seconds destory the enemy.
		Destroy (gameObject, 0f);

		Instantiate(explosion, this.transform.position, this.transform.rotation);
	}
}
