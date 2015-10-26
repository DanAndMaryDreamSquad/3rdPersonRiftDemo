using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class FollowCamera : MonoBehaviour {
    public GameObject target;
	public GameObject lastTransform;
    public float rotateSpeed;
    public float zoomSpeed;
    public float followDistance;
    public float followDistanceMax;
    public float followDistanceMin;
	public GameObject helpMenu;
    Vector3 offset;
    float angle = 0;
    float pitch = 30;
	float pendingAngle = 0;
	float pendingPitch = 30;
	bool pendingLookAtPlayer = false;
	bool pendingTopDownMode = false;
    bool canSwivel = false;
    bool lookAtPlayer = false;
	bool canChangePlayerLook = false;
	bool topDownMode = false;
    bool canChangeTopDown = false;
	Animator animator;
	bool isFading = false;
	PlayerHealth playerHealth;

    void Awake () {
        this.transform.eulerAngles = new Vector3 (45, 0, 0);
		animator = GetComponent<Animator>();
    }

    void LateUpdate () {
        GameManager.CameraMode mode = GameManager.instance.GetCameraMode ();

        bool topDownInput = Input.GetAxis ("TopDown") > 0.5f;
        // If the camera mode is MANUAL, The player can just "Look at Player" then pivot to the top to achieve this
        if (mode == GameManager.CameraMode.SEMI_AUTO && canChangeTopDown && topDownInput) {
			pendingTopDownMode = !topDownMode;
            canChangeTopDown = false;
			if (pendingTopDownMode) {
                pendingPitch = 85;
            } else {
				pendingPitch = 30;
			}
			StartFadingOut();
        }
        if (!topDownInput) {
            canChangeTopDown = true;
        }
        bool lookAtPlayerInput = Input.GetAxis ("LookAtPlayer") < -0.5f;
        // Player can't hit any buttons to change the Camera. That is one of the guarentees of AUTO cam mode
        if (mode == GameManager.CameraMode.MANUAL && canChangePlayerLook && lookAtPlayerInput) {
            pendingLookAtPlayer = !lookAtPlayer;
			canChangePlayerLook = false;
			StartFadingOut();		
        }
        if (!lookAtPlayerInput) {
            canChangePlayerLook = true;
        }

        switch (mode) {
        case GameManager.CameraMode.AUTO:
            AutoCamera();
            break;
        case GameManager.CameraMode.SEMI_AUTO:
            SemiAutoCamera();
            break;
        case GameManager.CameraMode.MANUAL:
            ManualCamera();
            break;
        }

		if (Input.GetButton("Help")) {
			helpMenu.SetActive(true);
		} else {
			helpMenu.SetActive(false);
		}


        
        //transform.LookAt(target.transform.position, Vector3.up);
        //Vector3 target = new Vector3(target.transform.position.x, 0, target.transform.position.z);
        //transform.position = target - (rotation * new Vector3(0f, 0f, followDistance));
        //transform.LookAt(target, Vector3.up);
    }

    void ManualCamera () {        
        float inputSwivel = Input.GetAxis ("Swivel");
        float inputPitch = Input.GetAxis ("Pitch");
        float mouseSwivel = -Input.GetAxis ("Mouse X");
        float mousePitch = Input.GetAxis ("Mouse Y");
        float followDistanceChange = Input.GetAxis ("Zoom");
        float mouseFollowDistanceChange = -Input.GetAxis ("Mouse ScrollWheel") * 100;
        if (Mathf.Abs (mouseFollowDistanceChange) > Mathf.Abs (followDistanceChange)) {
            followDistanceChange = mouseFollowDistanceChange;
        }
        followDistance = followDistance + (followDistanceChange * Time.deltaTime * zoomSpeed);
        followDistance = Mathf.Clamp (followDistance, followDistanceMin, followDistanceMax);
        
        if (Mathf.Abs (mouseSwivel) > Mathf.Abs (inputSwivel)) {
            inputSwivel = mouseSwivel;
        }
        if (Mathf.Abs (mousePitch) > Mathf.Abs (inputPitch)) {
            inputPitch = mousePitch;
        }
        
        //float desiredAngle = this.transform.eulerAngles.y;
        float desiredAngle = angle;
        if (inputSwivel != 0) {
            desiredAngle = desiredAngle + (-rotateSpeed * Time.deltaTime * inputSwivel);
        }
        
        //float desiredPitch = this.transform.eulerAngles.x;
        float desiredPitch = pitch;
        if (inputPitch != 0) {
            desiredPitch = desiredPitch + (-rotateSpeed * Time.deltaTime * inputPitch);
        }
        desiredPitch = Mathf.Clamp (desiredPitch, 0, 85);
        Quaternion rotation = Quaternion.Euler (desiredPitch, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * new Vector3 (0f, 0f, followDistance));
        pitch = desiredPitch;
        angle = desiredAngle;
        
        Vector3 forwardTarget = target.transform.position;
        
        // If VR is enabled, look straight rather than at the hero
        // Looking straight at the hero felt very dizzying
        if (VRSettings.enabled && (!lookAtPlayer && !topDownMode)) {
            forwardTarget.y = transform.position.y;
        }
		transform.LookAt (forwardTarget, Vector3.up);
		lastTransform.transform.position = transform.position;
		lastTransform.transform.rotation = transform.rotation;
    }

    void AutoCamera() {
        pitch = Mathf.Clamp (pitch, 0, 85);
        Quaternion rotation = Quaternion.Euler (pitch, angle, 0);
        transform.position = target.transform.position - (rotation * new Vector3 (0f, 0f, followDistance));        
        Vector3 forwardTarget = target.transform.position;
        
        // If VR is enabled, look straight rather than at the hero
        // Looking straight at the hero felt very dizzying
        if (VRSettings.enabled && !lookAtPlayer) {
			forwardTarget.y = transform.position.y;
        }
		transform.LookAt (forwardTarget, Vector3.up);
		if (Mathf.Abs(Input.GetAxis ("Horizontal")) < 0.12f && Mathf.Abs(Input.GetAxis ("Vertical")) < 0.12f) {
			lastTransform.transform.position = transform.position;
			lastTransform.transform.rotation = transform.rotation;
		}
    }

    void SemiAutoCamera() {
        float inputSwivel = -Input.GetAxis ("Swivel");
        float desiredAngle = angle;
        if (canSwivel && Mathf.Abs(inputSwivel) > 0.12f) {
            int direction = inputSwivel > 0 ? 1 : -1;
            pendingAngle = desiredAngle + (45 * direction);
			canSwivel = false;			
			StartFadingOut();
        }
        if (Mathf.Abs(inputSwivel) < 0.12f) {
            canSwivel = true;
        }
       
        pitch = Mathf.Clamp (pitch, 0, 85);
        Quaternion rotation = Quaternion.Euler (pitch, desiredAngle, 0);
        transform.position = target.transform.position - (rotation * new Vector3 (0f, 0f, followDistance));
        angle = desiredAngle;
        
        Vector3 forwardTarget = target.transform.position;
        
        // If VR is enabled, look straight rather than at the hero
        // Looking straight at the hero felt very dizzying
        if (VRSettings.enabled && (!lookAtPlayer && !topDownMode)) {
            forwardTarget.y = transform.position.y;
        }
		transform.LookAt (forwardTarget, Vector3.up);
		if (Mathf.Abs(Input.GetAxis ("Horizontal")) < 0.12f && Mathf.Abs(Input.GetAxis ("Vertical")) < 0.12f) {
			lastTransform.transform.position = transform.position;
			lastTransform.transform.rotation = transform.rotation;
		}
    }

	public void SetAngle(float angle) {
		GameManager.CameraMode mode = GameManager.instance.GetCameraMode ();
		if (mode != GameManager.CameraMode.AUTO) {
			return;
		}
		if (angle != pendingAngle) {
			this.pendingAngle = angle;
			StartFadingOut();
		}
    }

	public void FinishFade() {
		this.angle = pendingAngle;
		this.pitch = pendingPitch;
		this.lookAtPlayer = pendingLookAtPlayer;
		this.topDownMode = pendingTopDownMode;
		animator.SetTrigger("FadeIn");
		isFading = false;
	}

	void StartFadingOut() {
		if (!isFading) {
			animator.SetTrigger("FadeOut");
			isFading = true;
		}
	}

	public void DefeatFade(PlayerHealth health) {
		this.playerHealth = health;
		animator.SetTrigger("DefeatFadeOut");
    }

	public void EndDefeatFade() {
		playerHealth.EndRespawn();
		animator.SetTrigger("DefeatFadeIn");
	}
}
