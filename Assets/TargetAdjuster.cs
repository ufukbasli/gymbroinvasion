using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAdjuster : MonoBehaviour
{
    public PlayerReference playerReference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var player = playerReference.playerController;
        //Debug.Log(player.momentum.magnitude);
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,player.momentum.magnitude/9f);
    }
}
