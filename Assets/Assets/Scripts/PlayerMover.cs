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

    void Start () {
        animator = GetComponent<Animator> ();
        controller = GetComponent<CharacterController> ();
        lastDesiredFacingRotation = Quaternion.Euler (0, 45, 0);
    }

    void Update () {
        FindDirection ();
        Jumping ();
        Move ();
    }

    void FindDirection () {
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
        if (controller.isGrounded) {
            yMovement = 0;
            if (Input.GetButton ("Jump")) {
                yMovement = jumpSpeed;
                preJumpDesiredDirection = desiredDirection;
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

    void Move () {
        desiredDirection.y = yMovement;
        preJumpDesiredDirection.y = yMovement;
        if (controller.isGrounded) {
            controller.Move (desiredDirection * Time.deltaTime);
        } else {
            controller.Move (preJumpDesiredDirection * Time.deltaTime);
        }
        animator.SetBool ("IsWalking", isMoving);
    }

    public void Bounced () {
        //isBouncing = true;
    }

    public void Knocked (GameObject knocker) {
        Debug.Log ("inknocked");
        isBeingKnocked = true;
        desiredDirection = new Vector3 (
            this.transform.position.x - knocker.transform.position.x, 0,
            this.transform.position.z - knocker.transform.position.z);
        desiredDirection = desiredDirection.normalized;
        Debug.Log ("dd " + desiredDirection);

        this.transform.rotation = Quaternion.LookRotation (-desiredDirection, Vector3.up);

        desiredDirection *= speed;
        desiredDirection.y = jumpSpeed;
    }
}
