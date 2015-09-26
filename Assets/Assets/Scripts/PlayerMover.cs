using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float runAngle;
    public GameObject camera;
    private Vector3 desiredDirection = Vector3.zero;
    private CharacterController controller;
    private Animator animator;
    private bool isMoving = false;
    private bool fullStop = true;
    private bool isBouncing = false;
    public bool isBeingKnocked = false;
    private Quaternion lastDesiredFacingRotation = Quaternion.identity;
    private Quaternion defaultLeanRotation = Quaternion.Euler (new Vector3 (0, 0, 0));
    private Vector3 preJumpDesiredDirection = Vector3.zero;
    private float yMovement = 0;
	private bool knockedTick = false;
	private bool isGettingUp = false;

    void Start () {
        animator = GetComponent<Animator> ();
        controller = GetComponent<CharacterController> ();
        lastDesiredFacingRotation = Quaternion.Euler (0, 45, 0);
    }

    void Update () {
        FindDirection ();
        Jumping ();
		Knocking();
        Move ();
    }

    void FindDirection () {
		if (isBeingKnocked || isGettingUp) {
			return;
		}
        desiredDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
        if (desiredDirection.sqrMagnitude > 1) {
            desiredDirection = desiredDirection.normalized;
        }
        if (desiredDirection.sqrMagnitude == 0) {
            fullStop = true;
        }
        desiredDirection = camera.transform.TransformDirection (desiredDirection);
        desiredDirection.y = 0.0f;

        if ((fullStop && desiredDirection.sqrMagnitude > 0.0f) || (!isMoving && desiredDirection.sqrMagnitude > 0.05f) || (isMoving && desiredDirection.sqrMagnitude > 0.05f)) {
            Quaternion desiredFacingRotation = Quaternion.LookRotation (desiredDirection, Vector3.up);
            Quaternion desiredLeanRotation = Quaternion.Euler (new Vector3 (runAngle * desiredDirection.magnitude, 0, 0));
            this.transform.rotation = Quaternion.Slerp (this.transform.rotation, desiredFacingRotation * desiredLeanRotation, Time.deltaTime * 10);
            lastDesiredFacingRotation = desiredFacingRotation;
            isMoving = true;
        } else {
            this.transform.rotation = Quaternion.Lerp (this.transform.rotation, lastDesiredFacingRotation * defaultLeanRotation, Time.deltaTime * 10);
            fullStop = false;
            isMoving = false;
        }

        desiredDirection *= speed;

    }

    void Jumping () {
		if (isBeingKnocked) {
			yMovement -= gravity * Time.deltaTime;
			return;
		}
        if (controller.isGrounded) {
            yMovement = 0;
            preJumpDesiredDirection = desiredDirection;
            if (Input.GetButton ("Jump")) {
                yMovement = jumpSpeed;
            }
        } else {
            preJumpDesiredDirection = Vector3.Lerp (preJumpDesiredDirection, desiredDirection, Time.deltaTime);
        }
        if (isBouncing) {
            yMovement = jumpSpeed;
            isBouncing = false;
        }
        yMovement -= gravity * Time.deltaTime;
    }

	void Knocking() {
		if (!isBeingKnocked) {
			return;
		}
		if (controller.isGrounded && knockedTick) {
			desiredDirection = Vector3.zero;
			isBeingKnocked = false;
			knockedTick = false;
			isGettingUp = true;
			animator.SetTrigger("IsGettingUp");
		} else {
			knockedTick = true;
		}
	}

    void Move () {
        desiredDirection.y = yMovement;
        preJumpDesiredDirection.y = yMovement;
		if (isBeingKnocked) {
			controller.Move (desiredDirection * Time.deltaTime);
		} else if (controller.isGrounded) {
            controller.Move (desiredDirection * Time.deltaTime);
        } else {
            controller.Move (preJumpDesiredDirection * Time.deltaTime);
        }
        animator.SetBool ("IsWalking", isMoving);
    }

    public void Bounced () {
		Debug.Log("in bounce");
		if (!isBeingKnocked) {
			isBouncing = true;
		}
    }

    public void Knocked (GameObject knocker) {
        Debug.Log ("inknocked");
		if (isBeingKnocked) {
			return;
		}
        isBeingKnocked = true;
        desiredDirection = new Vector3 (
            this.transform.position.x - knocker.transform.position.x, 0,
            this.transform.position.z - knocker.transform.position.z);
        desiredDirection = desiredDirection.normalized;

        this.transform.rotation = Quaternion.LookRotation (-desiredDirection, Vector3.up);

        desiredDirection *= (speed / 2);
		yMovement = (jumpSpeed * 0.75f);
    }

	public void FinishedGettingUp () {
		isGettingUp = false;
	}
}
