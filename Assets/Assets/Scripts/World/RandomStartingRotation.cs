using UnityEngine;
using System.Collections;

public class RandomStartingRotation : MonoBehaviour {

	void Start() {
		this.transform.Rotate(Vector3.up, Random.Range(0, 3) * 90);
	}
}
