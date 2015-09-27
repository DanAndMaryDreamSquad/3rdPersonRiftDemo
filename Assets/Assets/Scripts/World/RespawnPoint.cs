using UnityEngine;
using System.Collections;

public class RespawnPoint : MonoBehaviour {
	
	public GameObject respawnBox;
	public GameObject fire;
	PlayerHealth playerHealth;
	bool touched = false;

	void Awake() {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();

	}

    void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !touched) {
			playerHealth.SetRespawnPoint(respawnBox);
			fire.SetActive(true);
			touched = true;
		}
	}

}
