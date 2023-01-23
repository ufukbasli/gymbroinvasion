using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Searching nearby enemies
    private List<baseCharacter> nearbyEnemies = new List<baseCharacter>();
    private baseCharacter nearestEnemy;
    public LayerMask characterMask;
    public float detectRange;

    public Animator _animator;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] public float _speed;
    [SerializeField] public float _agentSpeed;

    private baseCharacter movement;
    private int latestAttackIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<baseCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        //input gathering
        var forward = Camera.main.transform.forward;
        var right = Camera.main.transform.right;

        forward.y = 0;
        forward.Normalize();
        forward *= Input.GetAxis("Vertical");

        right.y = 0;
        right.Normalize();
        right *= Input.GetAxis("Horizontal");



        var input = forward + right;

        movement.MovePlayer(input,_speed);
        PlayerAnimations(_animator,movement.agent.velocity);
        //SearchingEnemies

        SearchTarget();
        SetTarget();

       
    }

    private void SearchTarget()
    {
        RaycastHit[] hitRays = Physics.SphereCastAll(transform.position, detectRange, transform.forward, detectRange, characterMask);

        if (hitRays.Length > 0)
        {

            for (int i = 0; i < hitRays.Length; i++)
            {
                var target = hitRays[i].transform.GetComponent<baseCharacter>();


                if (target != null)
                {

                    nearestEnemy = target;

                }
            }

        }
    }

    private void SetTarget()
    {
        if (nearbyEnemies.Count > 0)
        {
            nearestEnemy = nearbyEnemies[0];
            var nearestEnemyDistance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            for (int i = 0; i < nearbyEnemies.Count; i++)
            {
                var currentDistance = Vector3.Distance(transform.position, nearbyEnemies[i].transform.position);
                if (nearestEnemyDistance > currentDistance)
                {

                    nearestEnemy = nearbyEnemies[i];
                    nearestEnemyDistance = currentDistance;
                }

            }
        }


    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, detectRange);

        
    }

    public void PlayerAnimations(Animator animator, Vector3 direction)
    {
        //sprint Animations
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement.agent.speed =_sprintSpeed;
            animator.SetBool("isSprinting", true);
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            movement.agent.speed = _agentSpeed;
            animator.SetBool("isSprinting", false);
        }

        //attack Animations
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (latestAttackIndex > 0)
            {
                latestAttackIndex = 0;
            }
            else
            {
                latestAttackIndex = 1;
            }
            animator.SetInteger("attackIndex", latestAttackIndex);
            animator.SetBool("isAttacking", true);
        }
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetBool("isAttacking", false);
        }

        //run Animations
        if (direction.magnitude > 0f)
        {
            _animator.SetBool("isMoving", true);
        }
        else _animator.SetBool("isMoving", false);

    }
    
}
