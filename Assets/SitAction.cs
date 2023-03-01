using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitAction : MonoBehaviour
{
    public Transform sitPos;
    public BoxCollider col;

    private bool canSit;
    private bool isSitting;
    private baseCharacter player;
    private PlayerAnimations anim;
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            
            if (canSit)
            {
                if (!isSitting)
                {
                    col.enabled = false;
                    if (player != null)
                    {
                        anim._animator.SetTrigger("Sit");

                        player.agent.Warp(sitPos.position);
                        player.transform.LookAt(this.transform.forward);
                        player.agent.velocity = Vector3.zero;



                    }
                }
                else
                {
                    anim._animator.SetTrigger("StandUp");
                }
                
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        anim = other.GetComponent<PlayerAnimations>();
        player = other.GetComponent<baseCharacter>();
        if (player != null)
        {
            Debug.Log("playerEntered");
            canSit = true;
        }

        


            
    }
    private void OnTriggerExit(Collider other)
    {
        player = other.GetComponent<baseCharacter>();
        if (player != null)
        {
            
            canSit = false;
            player = null;
        }
    }
}
