using UnityEngine;
using System.Collections;

public class StartGameAction : MenuAction {

	public override void ActivateItem() {
        Application.LoadLevel("sandbox");
	}
}
