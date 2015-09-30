using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SemiAutoCameraAction : MenuAction {
	
	public Text helpText;
	
	public override void ActivateItem() {
		Application.LoadLevel("sandbox");
	}
	public override void OnCursorOver() {
		helpText.text = "You may toggle freely between preset camera positions.";
	}

}
