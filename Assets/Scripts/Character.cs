using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float hitPoint;
    public float speed;
    public float damage;
    private float _currentHitPoint;


    // Start is called before the first frame update
    void Start()
    {
        _currentHitPoint = hitPoint;
    }

    // Update is called once per frame
    void Update()
    {
        Die();
    }

    public void TakeDamage(float damage)
    {
        _currentHitPoint -= damage;
    }

    void Die()
    {
        if (_currentHitPoint <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void DealDamage(Character target)
    {
        target.TakeDamage(damage);
    }
    

}
