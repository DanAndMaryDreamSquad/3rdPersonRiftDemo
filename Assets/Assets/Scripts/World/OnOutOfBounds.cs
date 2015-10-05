using UnityEngine;
using System.Collections;

public class OnOutOfBounds : MonoBehaviour {
    
    PlayerHealth playerHealth;
    
    void Awake () {
        GameObject player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent<PlayerHealth> ();
    }

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            playerHealth.StartRespawn ();
        } else {
            Destroy (other.gameObject);
        }
    }
}
