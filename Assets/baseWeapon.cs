using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baseWeapon : MonoBehaviour
{
    //Gun stats
    public int damage;
    public float timeBetweenShooting, spread, timeBetweenShots;
    public int  bulletsPerTap;
    public bool allowButtonHold;
    int  bulletsShot;
    public Bullet bullet;
    public Transform muzzle;
    //bools 
    bool shooting, readyToShoot, reloading;




  

    private void Awake()
    {
        bullet.damage = damage;
        readyToShoot = true;
    }
    private void Update()
    {
        MyInput();

        
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        

        //Shoot
        if (readyToShoot && shooting )
        {
           
            Shoot();
        }
    }
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Instantiate(bullet, muzzle.position, transform.rotation);
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0)
            Invoke("Shoot", timeBetweenShots);


    }
    private void ResetShot()
    {
        readyToShoot = true;
    }
  
}
