using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public GameObject destroyEffect;
    public Character player;

    public float damage;
    private void Start()
    {
        Invoke("DestroyBullet",lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    void DestroyBullet()
    {
        Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        var target = other.GetComponent<baseCharacter>();

        if (target != null)
        {

            target.TakeDamage(damage);

        }

        DestroyBullet();
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
