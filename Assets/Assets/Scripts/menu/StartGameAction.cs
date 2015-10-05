using UnityEngine;
using System.Collections;

public class StartGameAction : MenuAction {

    public GameObject mainMenu;
    public GameObject cameraMenu;

	public override void ActivateItem() {
        mainMenu.SetActive(false);
        cameraMenu.SetActive(true);
	}
}
