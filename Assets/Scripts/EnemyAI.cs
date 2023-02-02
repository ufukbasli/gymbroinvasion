using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    


    private baseCharacter movement;
    private GameObject playerLocation;
    private NavMeshAgent agent;
    private attackModule gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = GetComponent<attackModule>();
        movement = GetComponent<baseCharacter>();
        agent = GetComponent<NavMeshAgent>();
        playerLocation = GameObject.FindGameObjectWithTag("Player");
        

    }

    // Update is called once per frame
    void Update()
    {
        var dir = playerLocation.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                agent.stoppingDistance = 10f;
                gun.Firing();
            }
            else
            {
                agent.stoppingDistance = 0f;
            }
        }
        movement.MoveTargetAI(playerLocation.transform.position);

        
    }

    
}
