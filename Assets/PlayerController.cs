using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
    public float maxSpeed = .3f;
    public float angleChangeSpeed = 90f;
    public float minimumMomentum = .1f;
    public float speedChange = .1f;
    private CharacterController characterController;
    private Camera cam;
    public Vector3 momentum;
    private void Awake() {
        characterController = GetComponent<CharacterController>();
        cam = Camera.main;
    }
    private void Update() {
        var input = GetInput();
        if (momentum.magnitude < minimumMomentum) {
            if (input.magnitude > .3f) momentum = input.normalized * minimumMomentum;
        }
        else {
            if (input.magnitude > .3f) {
                var angle = Vector3.Angle(momentum, input);
                if (angle <= 90) {
                    momentum = Vector3.RotateTowards(momentum, input, angleChangeSpeed * Mathf.Deg2Rad * Time.deltaTime, 0);
                }
                
                if (angle <= 45) {
                    momentum += momentum.normalized * (speedChange * Time.deltaTime);
                }
                else if (angle > 45 && angle <= 90) {
                }
                else {
                    momentum += momentum.normalized * (-speedChange * Time.deltaTime);

                }
            }
        }
        if (input.magnitude <= .3f) {
            momentum += momentum.normalized * (-speedChange * Time.deltaTime);
        }

        if (momentum.magnitude > maxSpeed) {
            momentum = momentum.normalized * maxSpeed;
        }
        characterController.Move(momentum);
        transform.LookAt(transform.position + momentum.normalized);
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
}