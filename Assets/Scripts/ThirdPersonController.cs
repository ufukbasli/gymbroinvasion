using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
   
    
    public Animator _animator;


    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _sprintSpeed = 15;
    [SerializeField] private float _turnSpeed = 360;
    [SerializeField] private ParticleSystem _rightPunch;
    [SerializeField] private ParticleSystem _leftPunch;
    public GameObject bonker;
    private float _startingSpeed = 5;
    private int latestAttackIndex=0;
    private Vector3 _input;

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        { 
            _speed = _sprintSpeed;
            _animator.SetBool("isSprinting", true);
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            _speed =_startingSpeed;
            _animator.SetBool("isSprinting", false);
        }
            
            
        Move();
    }

    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));


        //moving check
        if (_input.magnitude > Vector3.zero.magnitude)
        {
            _animator.SetBool("isMoving", true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }


        //attacking check
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (latestAttackIndex > 0)
            {
                latestAttackIndex = 0;

                _rightPunch.Play(true);
            }

            else
            {
                _leftPunch.Play(true);
                latestAttackIndex = 1; 
            }
            _animator.SetInteger("attackIndex", latestAttackIndex);
            _animator.SetBool("isAttacking",true);
        }
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            _animator.SetBool("isAttacking", false);
        }
        //heavy attack check
        if (Input.GetKey(KeyCode.Space))
        { 
            _animator.SetBool("isBonking", true);
            bonker.SetActive(true);
        }
        else
        {
            _animator.SetBool("isBonking", false);
            bonker.SetActive(false);
        }
        //ground Slam check
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.SetTrigger("isGroundSlam");
            
        }
       

    }

    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);
    }

    private void Move()
    {
        _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);
    }
    

}






