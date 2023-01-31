using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimations : MonoBehaviour
{
    public Animator _animator;
    [SerializeField] public NavMeshAgent agent;

    private baseCharacter deathController;
    // Start is called before the first frame update
    void Start()
    {
        deathController= GetComponent<baseCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.velocity.magnitude > 0.1f)
        {
            _animator.SetBool("isMoving",true);
        }
        else
        {
            _animator.SetBool("isMoving", false);
        }

        if (deathController.isDead)
        {
            KillEnemy();
        }
           
        
    }

    public void KillEnemy()
    {
        var deathIndex = Random.Range(0, 1);
        _animator.SetInteger("DeathIndex", deathIndex);
        _animator.SetTrigger("Die");
    }
}
