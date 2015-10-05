using UnityEngine;
using System.Collections;

public class ChangeMenuAction : MenuAction {
	
	public GameObject oldMenu;
	public GameObject newMenu;
	
	public override void ActivateItem() {
		oldMenu.SetActive(false);
		newMenu.SetActive(true);
	}
}
