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
    

    [SerializeField] private float _sprintSpeed;
    [SerializeField] public float _speed;
    [SerializeField] private float attackAnimRootDuration;
    private float lastAttackTime;
    

    private baseCharacter movement;
    private PlayerAnimations playerAnimatior;


    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<baseCharacter>();
        playerAnimatior = GetComponent<PlayerAnimations>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastAttackTime > attackAnimRootDuration)
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastAttackTime = Time.time;
                playerAnimatior.DoPunchAnim();
                movement.agent.velocity =Vector3.zero;
            }
            else
            {
                MoveUpdate();
            }
        }
        
        

        //SearchingEnemies

        SearchTarget();
        SetTarget();


    }

    private void MoveUpdate()
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

        var agentSpeed = Input.GetKey(KeyCode.LeftShift) ? _sprintSpeed : _speed;
        

        movement.MovePlayer(input, agentSpeed);
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

    
    
}
