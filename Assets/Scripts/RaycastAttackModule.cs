using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastAttackModule : MonoBehaviour
{
    [SerializeField]
    private float damage;
    
    [SerializeField]
    private float fireDistance;
    [SerializeField]
    private Transform bulletPoint;

    private float durationBtwShoots;
    [SerializeField]
    private float shootTimer;


    private RaycastHit hitInfo;


    private void Update()
    {
        if (durationBtwShoots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Fire();
                durationBtwShoots = shootTimer;

            }

        }
        else
        {
            durationBtwShoots -= Time.deltaTime;
        }
    }
    public void Fire()
    {
        Ray ray = new Ray();
        ray.origin = bulletPoint.position;
        ray.direction = bulletPoint.TransformDirection(Vector3.forward);

        Debug.DrawRay(ray.origin, ray.direction * fireDistance, Color.green);

        if(Physics.Raycast(ray,out hitInfo, fireDistance))
        {
            var enemy = hitInfo.collider.GetComponent<Character>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
