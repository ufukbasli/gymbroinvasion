using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    public void SetMomentum(float momentum)
    {
        anim.SetFloat("movementSpeed", momentum);
    }
}
