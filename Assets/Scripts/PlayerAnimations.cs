using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimations : MonoBehaviour
{
    public Animator _animator;

    [SerializeField] public NavMeshAgent agent;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("movementSpeed", agent.velocity.magnitude);
    }
    
    public void DoPunchAnim()
    {
       var nextAttackAnim= (_animator.GetInteger("attackIndex") +1)%2;
        _animator.SetInteger("attackIndex", nextAttackAnim);
        _animator.SetTrigger("Attack");
    }
}
