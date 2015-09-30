using UnityEngine;
using System.Collections;

public class ExitGameAction : MenuAction
{

	public override void ActivateItem ()
	{
		if (Application.isEditor) {
			UnityEditor.EditorApplication.isPlaying = false;
		} else {
			Application.Quit ();
		}
	}
}
