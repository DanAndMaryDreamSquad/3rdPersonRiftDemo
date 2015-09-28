using UnityEngine;
using System.Collections;

public class Oscillator : MonoBehaviour {

	Vector3 origin;
	public float from = 5f;
	public float to = -5f;
	public float speed = 1f;
	float timeOffset;

	void Awake() {
		origin = this.transform.position;
		timeOffset = Random.Range(0, speed * 1000);
	}

	void Update() {
		float t = (Mathf.Sin ((Time.time + timeOffset) * speed * Mathf.PI * 2.0f) + 1.0f) / 2.0f;
		float offset = Mathf.Lerp (from, to, t);
		Vector3 result = new Vector3(origin.x, origin.y + offset, origin.z);
		this.transform.position = result;
	}
}
