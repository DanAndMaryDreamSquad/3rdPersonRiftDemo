using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    
public class GameManager : MonoBehaviour {
        
    // singleton pattern from https://unity3d.com/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager?playlist=17150

    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public enum CameraMode {
        AUTO,
        SEMI_AUTO,
        MANUAL
    }
	CameraMode cameraMode = CameraMode.SEMI_AUTO;

    //Awake is always called before any Start functions
    void Awake () {
        //Check if instance already exists
        if (instance == null) {             
            //if not, set instance to this
            instance = this;
        } else if (instance != this) {   //If instance already exists and it's not this:            
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy (gameObject);    
        }
            
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad (gameObject);
                        
        //Call the InitGame function to initialize the first level 
        InitGame ();
    }
        
    //Initializes the game for each level.
    void InitGame () {
    }

    public void SetCameraMode(CameraMode cameraMode) {
        this.cameraMode = cameraMode;
    }

	public CameraMode GetCameraMode() {
		return cameraMode;
	}
}
