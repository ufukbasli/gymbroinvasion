using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackModule : MonoBehaviour
{
    public GameObject bullet;
    
    public Transform muzzle;
    public float startTimeBtwShots;

    private float timeBtwShots;
    // Start is called before the first frame update
    void Start()
    {
        timeBtwShots = 0;
    }

    // Update is called once per frame
    void Update()
    {

        

        if (timeBtwShots <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(bullet, muzzle.position, transform.rotation);

                timeBtwShots = startTimeBtwShots;
            }

            
        }
        else
        {

            timeBtwShots -= Time.deltaTime;
            
            
        }
        
    }

   
}
