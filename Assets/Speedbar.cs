using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedbar : MonoBehaviour
{
    public PlayerReference playerReference;
    public Image speedbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var player = playerReference.playerController;
        var speed = player.momentum.magnitude / player.maxSpeed;
        speedbar.fillAmount = speed;
    }
}
