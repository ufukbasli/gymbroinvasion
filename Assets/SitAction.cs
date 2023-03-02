using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitAction : MonoBehaviour
{
    public Transform sitPos;
    public BoxCollider col;

    private bool canSit;
    private bool isSitting;
    private baseCharacter sittingPlayer;
    private PlayerAnimations anim;
    private List<baseCharacter> players = new List<baseCharacter>();
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (sittingPlayer == null)
            {
                if (players.Count > 0)
                {
                    sittingPlayer= players[0];
                    sittingPlayer.agent.velocity = Vector3.zero;
                    sittingPlayer.agent.Warp(sitPos.position);
                    //player.transform.position = sitPos.position;
                    sittingPlayer.transform.LookAt(this.transform.forward);
                    anim=sittingPlayer.gameObject.GetComponent<PlayerAnimations>();
                    anim._animator.SetTrigger("Sit");

                    var movement = sittingPlayer.gameObject.GetComponent<PlayerMovement>();
                    movement.canMove = false;


                }
            }
            else
            {
                anim._animator.SetTrigger("StandUp");
                var movement = sittingPlayer.gameObject.GetComponent<PlayerMovement>();
                movement.canMove = true;
                sittingPlayer = null;

                
            }

            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        var player = other.GetComponent<baseCharacter>();
        if (player != null)
        {
            players.Add(player);
            
            
            
        }

        


            
    }
    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<baseCharacter>();
        if (player != null)
        {
            players.Remove(player);
            
        }
    }
}
