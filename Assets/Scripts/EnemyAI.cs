using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float enemyDamage;
    public float enemyAttackSpeed;


    private baseCharacter movement;
    private GameObject playerLocation;
    private baseCharacter player;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<baseCharacter>();

        playerLocation = GameObject.FindGameObjectWithTag("Player");
        player= playerLocation.GetComponent<baseCharacter>();

    }

    // Update is called once per frame
    void Update()
    {
        movement.MoveTargetAI(playerLocation.transform.position);

        if (movement.agent.pathPending)
        {
            if (movement.agent.remainingDistance <= movement.agent.stoppingDistance)
            {
                if (!movement.agent.hasPath || movement.agent.velocity.sqrMagnitude == 0f)
                {
                    //Invoke("EnemyAttack", enemyAttackSpeed);
                }
            }
        }
    }

    private void EnemyAttack()
    {
        player.TakeDamage(enemyDamage);
    }
}
