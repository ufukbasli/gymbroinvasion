using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class baseCharacter : MonoBehaviour
{

    public NavMeshAgent agent;
    public Image healthbar;
    public float hitPoint;
    public bool isDead;



    private float _currentHitPoint;
    
    


    void Start()
    {
        _currentHitPoint = hitPoint;
        isDead = false;



    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Die();
        UpdateHealthBar();
        
    }

    public void MoveTargetAI(Vector3 target)
    {
        agent.SetDestination(target);
    }
    public void MovePlayer(Vector3 input,float targetSpeed)
    {
        targetSpeed = input.magnitude > 0 ? targetSpeed : 0;
        var speed = Mathf.MoveTowards(agent.velocity.magnitude, targetSpeed, agent.acceleration*Time.deltaTime);

        var direction = input.normalized;
        agent.velocity = direction * speed;
    }

    
    public void TakeDamage(float damage)
    {
        _currentHitPoint -= damage;
        
    }

    void Die()
    {
        if (_currentHitPoint <= 0f)
        {
            isDead = true;
            Destroy(gameObject, 2);
        }
    }
    
    private void UpdateHealthBar()
    {
        healthbar.fillAmount = Mathf.Clamp(_currentHitPoint / hitPoint, 0, 1f);
        
    }


}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}