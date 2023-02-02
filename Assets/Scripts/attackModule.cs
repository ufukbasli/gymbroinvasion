using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class attackModule : MonoBehaviour
{
    public GameObject bullet;
    
    public Transform muzzle;
    public float startTimeBtwShots;
    public bool canFire { get; set; }
    private float timeBtwShots;
    private NavMeshAgent agent;
    private EnemyAnimations _animator;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<EnemyAnimations>();
        timeBtwShots = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (timeBtwShots <= 0f)
        {
            canFire = true;
            
        }
        else
        {

            timeBtwShots -= Time.deltaTime;


        }



    }

    public void Firing()
    {
        if (canFire)
        {
            _animator.FiringAnim();
            Instantiate(bullet, muzzle.position, transform.rotation);

            timeBtwShots = startTimeBtwShots;

            canFire = false;
            Debug.Log("fired");
        }
        
        
    }
   
}
