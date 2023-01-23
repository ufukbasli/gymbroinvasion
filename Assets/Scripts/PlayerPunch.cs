using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    //punch modifiers
    public float fovDistance;
    public float punchAngle;
    public float punchDmg;
    public float punchTimer;
    public float punchDelay;
    public GameObject punchCollision;

    private float currentPunchTimer;
    private GameObject punchObject;
    private baseCharacter enemy;




    private void Update()
    {
        if (currentPunchTimer <= 0f)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PunchTarget();

                if (punchObject != null)
                {
                    var rotation = Quaternion.LookRotation(punchObject.transform.position - transform.position);
                    rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
                    transform.rotation = rotation;
                    //Debug.Log(punchObject);
                    enemy = punchObject.GetComponent<baseCharacter>();
                    Debug.Log(enemy);
                    if (enemy != null)
                    {
                        
                        
                    }
                }

            }
        }
        else
        {
            currentPunchTimer -= Time.deltaTime;
        }

        //Debug.DrawRay(transform.position, transform.forward);
        //Debug.DrawRay(transform.position, Quaternion.AngleAxis(punchAngle, Vector3.up) * transform.forward);
        //Debug.DrawRay(transform.position, Quaternion.AngleAxis(-punchAngle, Vector3.up) * transform.forward);

    }

    private void DealDamage()
    {
        enemy.TakeDamage(punchDmg);
        
        enemy = null;
    }
    private void PunchTarget()
    {
        Ray centerRay = new Ray(transform.position, transform.forward);
        var leftDirection = Quaternion.AngleAxis(punchAngle, Vector3.up) * transform.forward;
        Ray leftRay = new Ray(transform.position, leftDirection);
        var rightDirection = Quaternion.AngleAxis(-punchAngle, Vector3.up) * transform.forward;
        Ray rightRay = new Ray(transform.position, rightDirection);

        RaycastHit centerHit;
        RaycastHit leftHit;
        RaycastHit rightHit;

        
        if (Physics.Raycast(centerRay, out centerHit, fovDistance))
        {
            punchObject = centerHit.collider.gameObject;
        }
        else
        {
            if(Physics.Raycast(leftRay, out leftHit, fovDistance))
            {
                punchObject = leftHit.collider.gameObject;
            }
            else
            {
                if (Physics.Raycast(rightRay, out rightHit, fovDistance))
                {
                    punchObject = rightHit.collider.gameObject;
                }
                else punchObject = null;
            }
        }

    }
    
}
