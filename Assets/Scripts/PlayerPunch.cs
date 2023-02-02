using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    //punch modifiers
    
    public float punchDmg;
 
    public GameObject punchCollision;

    private void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.GetComponent<baseCharacter>();

        

        if (enemy != null)
        {
            CameraShake.Instance.CameraShaker();
            enemy.TakeDamage(punchDmg,this.transform.position);
        }
    }
    
}
