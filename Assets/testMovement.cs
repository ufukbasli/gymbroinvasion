using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var dir = GetInput();
        rb.AddForce(dir*5f);
    }
    private Vector3 GetInput()
    {
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
