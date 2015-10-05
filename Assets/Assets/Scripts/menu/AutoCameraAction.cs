using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AutoCameraAction : MenuAction {
	
	public Text helpText;

    public override void ActivateItem() {
        GameManager.instance.SetCameraMode(GameManager.CameraMode.AUTO); 
		Application.LoadLevel("sandbox");
	}
	public override void OnCursorOver() {
		helpText.text = "The game will automatically position the camera as you play.\nThis is the most comfortable option.";
	}

}
