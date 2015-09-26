using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class RiftUpdater : MonoBehaviour {

	void Update () {
		this.transform.position = this.transform.position + InputTracking.GetLocalPosition(VRNode.Head);
		Debug.Log(InputTracking.GetLocalPosition(VRNode.Head));
	}
}
