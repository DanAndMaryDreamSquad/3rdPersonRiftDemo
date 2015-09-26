using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	public GameObject target;
	public float rotateSpeed;
	public float followDistance;
	Vector3 offset;
	
	void Start() {
		this.transform.eulerAngles = new Vector3(45, 0, 0);
	}
	void LateUpdate() {
		float desiredAngle = this.transform.eulerAngles.y;
		if (Input.GetAxis("Swivel") != 0) {
			desiredAngle = desiredAngle + (-rotateSpeed * Time.deltaTime * Input.GetAxis("Swivel") );
		}		
		float desiredPitch = this.transform.eulerAngles.x;
		if (Input.GetAxis("Pitch") != 0) {
			desiredPitch = desiredPitch + (-rotateSpeed * Time.deltaTime * Input.GetAxis("Pitch") );
		}
		desiredPitch = Mathf.Clamp(desiredPitch, 0, 85);
		Quaternion rotation = Quaternion.Euler(desiredPitch, desiredAngle, 0);
		transform.position = target.transform.position - (rotation * new Vector3(0f, 0f, followDistance));
		transform.LookAt(target.transform.position, Vector3.up);
		
		//Vector3 target = new Vector3(target.transform.position.x, 0, target.transform.position.z);
		//transform.position = target - (rotation * new Vector3(0f, 0f, followDistance));
		//transform.LookAt(target, Vector3.up);
	}
}
