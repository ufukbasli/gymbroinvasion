using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public float maxSpeed = .3f;
    public float angleChangeSpeed = 90f;
    public float minimumMomentum = .1f;
    public float speedChangeAcceleration = .1f;
    public float speedChangeBrake = .1f;
    public float hitSpeedChange = .1f;
    public PlayerReference playerReference;
    private CharacterController characterController;
    private Camera cam;
    private PlayerAnimations anim;
    [NonSerialized] public Vector3 momentum;
    private Rigidbody body;
    private void Awake() {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        playerReference.playerController = this;
        body = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnimations>();

    }
    private void Update() {
        var input = GetInput();
        if (momentum.magnitude < minimumMomentum) {
            if (input.magnitude > .3f) momentum = input.normalized * minimumMomentum;
        }
        else {
            if (input.magnitude > .3f) {
                var angle = Vector3.Angle(momentum, input);
                if (angle <= 135) {
                    momentum = Vector3.RotateTowards(momentum, input, angleChangeSpeed * Mathf.Deg2Rad * Time.deltaTime, 0);
                }
                
                if (angle <= 45) {
                    momentum += momentum.normalized * (speedChangeAcceleration * Time.deltaTime);
                    //speeding up
                }
                else if (angle >= 90) {
                    momentum += momentum.normalized * (-speedChangeBrake * Time.deltaTime);
                    //sliding
                }

            }
        }
        if (input.magnitude <= .3f) {
            momentum += momentum.normalized * (-speedChangeBrake * Time.deltaTime);
            
        }
        if (momentum.magnitude > maxSpeed) {
            momentum = momentum.normalized * maxSpeed;
            
        }
        
        characterController.Move(momentum + Vector3.down);
        
        if (momentum.magnitude > minimumMomentum) {
            transform.LookAt(transform.position + momentum.normalized);
        }
        body.angularVelocity = Vector3.zero;

        Debug.Log(momentum.magnitude);
        anim.SetMomentum(momentum.magnitude);
    }
    private Vector3 GetInput() {
        var forward = cam.transform.forward;
        var right = cam.transform.right;
        forward.y = 0;
        forward.Normalize();
        forward *= Input.GetAxis("Vertical");
        right.y = 0;
        right.Normalize();
        right *= Input.GetAxis("Horizontal");
        var input = forward + right;
        return Vector3.ClampMagnitude(input, 1f);
    }
    public void Hit(EnemyMan man) {
        momentum += momentum.normalized * (-hitSpeedChange);
    }
    private void OnDestroy() {
        if (playerReference.playerController == this) {
            playerReference.playerController = null;
        }
    }
}