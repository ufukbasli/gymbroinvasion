using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    public CinemachineVirtualCamera cam;
    public float shakeAmount;
    public float shakeLerp;

    // Start is called before the first frame update

    private void Awake()
    {
        Instance = this;
    }
    public void CameraShaker()
    {
        var noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        noise.m_AmplitudeGain = Mathf.Lerp(0f, shakeAmount, shakeLerp);

        Invoke("ShakeReset", shakeLerp);

    }

    private void ShakeReset()
    {
        var noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        noise.m_AmplitudeGain = 0f;

    }
    // Update is called once per frame
    
}
