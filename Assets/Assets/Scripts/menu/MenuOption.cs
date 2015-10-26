using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuOption : MonoBehaviour {

	Text menuText;
	MenuAction menuAction;

	void Awake () {
		menuText = GetComponent<Text>();
		menuAction = GetComponent<MenuAction>();
	}

	public Vector3 GetAnchorPosition() {
		return new Vector3(transform.position.x - (menuText.preferredWidth / 2), transform.position.y, transform.position.z);
	}

	public void ActivateItem() {
		menuAction.ActivateItem();
	}

	public void OnCursorOver() {
		menuAction.OnCursorOver();
	}
}
