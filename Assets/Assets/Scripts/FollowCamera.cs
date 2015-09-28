using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	public GameObject target;
	public float rotateSpeed;
	public float zoomSpeed;
	public float followDistance;
	public float followDistanceMax;
	public float followDistanceMin;
	Vector3 offset;
	
	void Start() {
		this.transform.eulerAngles = new Vector3(45, 0, 0);
	}
	void LateUpdate() {
		float desiredAngle = this.transform.eulerAngles.y;
		float inputSwivel = Input.GetAxis("Swivel");
		float inputPitch = Input.GetAxis("Pitch");
		float mouseSwivel = -Input.GetAxis("Mouse X");
		float mousePitch = Input.GetAxis("Mouse Y");
		float followDistanceChange = Input.GetAxis("Zoom");
		float mouseFollowDistanceChange = -Input.GetAxis("Mouse ScrollWheel") * 100;
		if (Mathf.Abs(mouseFollowDistanceChange) > Mathf.Abs(followDistanceChange)){
			followDistanceChange = mouseFollowDistanceChange;
		}
		followDistance = followDistance + (followDistanceChange * Time.deltaTime * zoomSpeed);
		followDistance = Mathf.Clamp(followDistance, followDistanceMin, followDistanceMax);

		if (Mathf.Abs(mouseSwivel) > Mathf.Abs(inputSwivel)) {
			inputSwivel = mouseSwivel;
		}
		if (Mathf.Abs(mousePitch) > Mathf.Abs(inputPitch)) {
			inputPitch = mousePitch;
		}
		if (inputSwivel != 0) {
			desiredAngle = desiredAngle + (-rotateSpeed * Time.deltaTime * inputSwivel );
		}		
		float desiredPitch = this.transform.eulerAngles.x;
		if (inputPitch != 0) {
			desiredPitch = desiredPitch + (-rotateSpeed * Time.deltaTime * inputPitch );
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
