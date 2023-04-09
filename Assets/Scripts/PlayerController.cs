using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public float maxSpeed = .3f;
    public float angleChangeSpeed = 90f;
    public float minimumMomentum = .1f;
    public float speedChangeAcceleration = .1f;
    public float speedChangeBrake = .1f;
    public float hitSpeedChange = .1f;
    public float slideBoost;
    public float slideDeplateRate;
    public float slideRechargeRate;
    public PlayerReference playerReference;
    public Transform wallDetector;
    public LayerMask wallBounceLayerMask;
    public float wallBounceDistance = 1f;
    public GameObject gameCam;
    public FloatingJoystick joystick;
    private CharacterController characterController;
    private Camera cam;
    private PlayerAnimations anim;
    private Vector3 defaultMovement;
    private float currentSlideBoost;
    private bool isSliding;
    private float slideRatio;
    [NonSerialized] public Vector3 momentum;
    private Rigidbody body;
    public bool locked { get; set; }
    private void Awake() {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
        playerReference.playerController = this;
        body = GetComponent<Rigidbody>();
        anim = GetComponent<PlayerAnimations>();
        currentSlideBoost = slideBoost;
        isSliding = false;
    }
    private void Update() {
        WallCollideSystemUpdate();
        
        var input = GetInput();
        
        if (locked) {
            input = Vector3.zero;
        }
        if (momentum.magnitude < minimumMomentum) {
            if (input.magnitude > .3f) momentum = input.normalized * minimumMomentum;
        }
        else {
            if (input.magnitude > .3f) {
                var angle = Vector3.Angle(momentum, input);
                if (angle <= 135) momentum = Vector3.RotateTowards(momentum, input, angleChangeSpeed * Mathf.Deg2Rad * Time.deltaTime, 0);

                if (angle <= 45)
                    momentum += momentum.normalized * (speedChangeAcceleration * Time.deltaTime);
                //speeding up
                else if (angle >= 90) momentum += momentum.normalized * (-speedChangeBrake * Time.deltaTime);
                //sliding
            }
        }

        if (input.magnitude <= .3f) momentum += momentum.normalized * (-speedChangeBrake * Time.deltaTime);
        if (momentum.magnitude > maxSpeed) momentum = momentum.normalized * maxSpeed;

        //sliding states 
        if (!isSliding && Input.GetKeyDown(KeyCode.LeftShift) && momentum.magnitude > 7) isSliding = true;
        if ((isSliding && Input.GetKeyUp(KeyCode.LeftShift) && currentSlideBoost > 1) || (isSliding && currentSlideBoost < .5)) isSliding = false;

        //sliding calculations
        if (isSliding) {
            currentSlideBoost -= slideDeplateRate * Time.deltaTime;
            currentSlideBoost = Mathf.Clamp(currentSlideBoost, 0, slideBoost);
            anim.SlidingTrue(true);
            slideRatio = currentSlideBoost;
        }
        else {
            anim.SlidingTrue(false);
            if (currentSlideBoost < slideBoost)
                currentSlideBoost += slideRechargeRate * Time.deltaTime;
            else
                currentSlideBoost = slideBoost;
            slideRatio = 1;
        }

        if (momentum.magnitude >= 6)
        {
            CamChange(false, gameCam);
        }
        else 
        {
            CamChange(true, gameCam);
        }
        characterController.Move((momentum + Vector3.down) * Time.deltaTime * slideRatio);

        if (momentum.magnitude > minimumMomentum) transform.LookAt(transform.position + momentum.normalized);
        body.angularVelocity = Vector3.zero;

        anim.SetMomentum(momentum.magnitude);
    }
    private RaycastHit[] hits = new RaycastHit[10];
    private bool inputDisabled;

    private void WallCollideSystemUpdate()
    {
        var rayStart = wallDetector.position;
        var rayDirection = wallDetector.forward;
        var ray = new Ray(rayStart, rayDirection);

        
        var hitCount = Physics.RaycastNonAlloc(ray, hits, wallBounceDistance, wallBounceLayerMask);

        Debug.DrawRay(rayStart, rayDirection * wallBounceDistance, Color.green);

        if (hitCount > 0)
        {
            // Disable movement
            locked = true;
            inputDisabled = true;
            momentum = Vector3.zero;

            // Wait for 0.2 seconds
            StartCoroutine(BounceOffWallCoroutine(0.2f));
        }
    }

    private IEnumerator BounceOffWallCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        var hit = hits[0];
        var normal = hit.normal;

        // Bounce off the wall
        var bounceDirection = Vector3.Reflect(momentum.normalized, normal);
        Debug.Log(Vector3.Reflect(momentum.normalized, normal));
        momentum = bounceDirection * momentum.magnitude;
        
        // Rotate towards the new momentum direction
        transform.rotation = Quaternion.LookRotation(bounceDirection);

        // Enable movement after bouncing
        locked = false;
        inputDisabled = false;
    }


    private Vector3 GetInput() {
        if (!inputDisabled)
        {
            var forward = cam.transform.forward;
            var right = cam.transform.right;
            forward.y = 0;
            forward.Normalize();
            forward *= joystick.Vertical;
            right.y = 0;
            right.Normalize();
            right *= joystick.Horizontal;
            var input = forward + right;
            return Vector3.ClampMagnitude(input, 1f);
        }
        else
        {
            return Vector3.zero;
        }
    }
    public void Hit() {
        momentum += momentum.normalized * -hitSpeedChange;
    }
    private void OnDestroy() {
        if (playerReference.playerController == this) playerReference.playerController = null;
    }

    private void CamChange(bool result,GameObject cam)
    {
        cam.SetActive(result);
    }
}