using UnityEngine;
using System.Collections;

public class PlatformMover : MonoBehaviour {
	
	public Transform offset;
	public float pauseTime;
	public float moveSpeed;
	Vector3 origin;
	Vector3 target;
	bool towards = true;
	bool pause = true;
	float pauseTimer = 0;

	void Awake() {
		origin = this.transform.position;
		target = this.transform.position + offset.localPosition;
	}

	void Update() {
		if (pause) {
			pauseTimer += Time.deltaTime;
			if (pauseTimer > pauseTime) {
				pause = false;
			}
			return;
		}
		if (towards) {
			this.transform.position = Vector3.MoveTowards(this.transform.position, target, Time.deltaTime * moveSpeed);
			if (transform.position.Equals(target)) {
				towards = false;
				pause = true;
				pauseTimer = 0;
			}
		} else {
			this.transform.position = Vector3.MoveTowards(this.transform.position, origin, Time.deltaTime * moveSpeed);
			if (transform.position.Equals(origin)) {
				towards = true;
				pause = true;
				pauseTimer = 0;
			}
		}
	}

}
