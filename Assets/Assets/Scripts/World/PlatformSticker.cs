using UnityEngine;
using System.Collections;

public class PlatformSticker : MonoBehaviour {

	GameObject player;
	GameObject parentPlatform;

	void Awake () {
		player = GameObject.FindGameObjectWithTag("Player");
		parentPlatform = gameObject.transform.parent.gameObject;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			player.transform.parent = parentPlatform.transform;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			player.transform.parent = null;
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.tag == "Player") {
			player.transform.parent = parentPlatform.transform;
		}
	}
}
