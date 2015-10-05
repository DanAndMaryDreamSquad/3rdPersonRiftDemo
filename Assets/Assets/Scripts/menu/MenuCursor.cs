using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MenuCursor : MonoBehaviour {

    public List<MenuOption> options;
    MenuOption currentSelection;
    float zOffset = 16f;
    bool canMove = false;
    bool canSelect = false;
    float speed = 5f;

    // Use this for initialization
    void Start () {
        currentSelection = options [0];
    }
    
    // Update is called once per frame
    void Update () {
        float input = Input.GetAxisRaw ("Vertical");
        int index = options.IndexOf (currentSelection);
        if (canMove) {
            if (input > 0) {
                index = index - 1;
                canMove = false;
            } else if (input < 0) {
                index = index + 1;
                canMove = false;
            }
        }
        index = mod(index, options.Count);
        if (input == 0) {
            canMove = true;
		}
		if (currentSelection != options[index]) {
			currentSelection = options[index];
			currentSelection.OnCursorOver();
		}

        Vector3 target = currentSelection.GetAnchorPosition ();
        target.z = target.z + zOffset;
        this.transform.position = Vector3.Lerp(this.transform.position, target, Time.deltaTime * speed);

        if (canSelect && Input.GetButton ("Jump")) {
            currentSelection.ActivateItem();
            canSelect = false;
        }
        if (!Input.GetButton ("Jump")) {
            canSelect = true;
        }

    }

    int mod(int x, int m) {
        return (x % m + m) % m;
    }
}
