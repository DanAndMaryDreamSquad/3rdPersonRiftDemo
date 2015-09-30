using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class OculusResetter : MonoBehaviour {

	void Update () {
		if (Input.GetButton("Cancel")) {
			Debug.Log("recentering");
		    InputTracking.Recenter();
		}
	}
}
