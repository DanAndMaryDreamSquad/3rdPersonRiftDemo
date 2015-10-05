using UnityEngine;
using System.Collections;

public class CameraAnglePad : MonoBehaviour {

	public FollowCamera followCamera;

	void Start() {
		if (GameManager.instance.GetCameraMode() != GameManager.CameraMode.AUTO) {
			this.gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			followCamera.SetAngle(this.transform.eulerAngles.y);
		}
	}
}
