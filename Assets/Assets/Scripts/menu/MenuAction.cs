using UnityEngine;
using System.Collections;

public abstract class MenuAction : MonoBehaviour {

	public abstract void ActivateItem();

	public virtual void OnCursorOver() { }

}
