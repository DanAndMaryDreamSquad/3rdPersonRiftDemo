using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManualCameraAction : MenuAction {
	
	public Text helpText;
	
    public override void ActivateItem() {
        GameManager.instance.SetCameraMode(GameManager.CameraMode.MANUAL); 
		Application.LoadLevel("sandbox");
	}
	public override void OnCursorOver() {
		helpText.text = "Full control over the camera's position,\nmuch like a classic third-person game." 
			+ "\nThis mode may be very uncomfortable and dizzying.\nUse at your own risk!";
	}

}
